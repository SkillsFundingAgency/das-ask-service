using System.ComponentModel;

namespace SFA.DAS.ASK.Data.Entities
{
    public enum ActivityType
    {
        [Description("Planning meeting")]
        PlanningMeeting,
        [Description("Awareness assembly")]
        AwarenessAssembly,
        [Description("Parents event")]
        ParentsEvent,
        [Description("Careers fair attendance")]
        CareersFairAttendance,
        [Description("Teacher training and development")]
        TeacherTraining,
        [Description("Registration & application workshop")]
        RegistrationAndApplicationWorkshop,
        [Description("Mock assessment centre workshop")]
        MockAssessmentCentreWorkshop,
        [Description("Staff specialist disadvantaged programme")]
        StaffSpecialistDisadvantagedProgramme
    }
}