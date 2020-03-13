using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using SFA.DAS.ASK.Application.Handlers.DeliveryPartner.SignInDeliveryPartnerContact;

namespace SFA.DAS.ASK.Web.Infrastructure.Authentication
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddDfeAuthentication(this IServiceCollection services, IHostingEnvironment environment, IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.Cookie.Name = ".Ask.Cookies";
                    options.Cookie.HttpOnly = true;

                    if (!environment.IsDevelopment())
                    {
                        options.Cookie.Domain = ".apprenticeships.education.gov.uk";
                    }

                    options.SlidingExpiration = true;
                    options.ExpireTimeSpan = TimeSpan.FromHours(1);
                })
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
                {

                    options.SignInScheme = "Cookies";
                    options.Authority = configuration["DfeSignIn:MetadataAddress"];
                    options.RequireHttpsMetadata = false;
                    options.ClientId = configuration["DfeSignIn:ClientId"];
                    options.ClientSecret = configuration["DfeSignIn:ClientSecret"];
                    options.ResponseType = OpenIdConnectResponseType.Code;
                    options.GetClaimsFromUserInfoEndpoint = true;
                    
                    
                    options.Scope.Clear();
                    options.Scope.Add("openid");
                    options.Scope.Add("email");
                    options.Scope.Add("profile");

                    options.SaveTokens = true;
                    options.UseTokenLifetime = true;
                    
                    options.SecurityTokenValidator = new JwtSecurityTokenHandler
                    {
                        InboundClaimTypeMap = new Dictionary<string, string>(),
                        TokenLifetimeInMinutes = 20,
                        SetDefaultTimesOnTokenCreation = true,
                    };
                    options.ProtocolValidator = new OpenIdConnectProtocolValidator
                    {
                        RequireSub = true,
                        RequireStateValidation = false,
                        NonceLifetime = TimeSpan.FromMinutes(15)
                    };
                    
                    options.DisableTelemetry = true;
                    options.Events = new OpenIdConnectEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var isSpuriousAuthCbRequest =
                                context.Request.Path == options.CallbackPath &&
                                context.Request.Method == "GET" &&
                                !context.Request.Query.ContainsKey("code");

                            if (isSpuriousAuthCbRequest)
                            {
                                context.HandleResponse();
                                context.Response.StatusCode = 302;
                                context.Response.Headers["Location"] = "/";
                            }

                            return Task.CompletedTask;
                        },

                        OnRemoteFailure = ctx =>
                        {
                            return Task.FromResult(0);
                        },

                        OnTokenValidated = context =>
                        {
                            var sp = services.BuildServiceProvider();
                            var mediator = sp.GetService<IMediator>();

                            mediator.Send(new SignInDeliveryPartnerContactRequest(Guid.Parse(context.Principal.FindFirst("sub").Value)), CancellationToken.None).Wait();
                            
                            return Task.CompletedTask;
                        }
                    };
                });

            return services;
        }
    }
}