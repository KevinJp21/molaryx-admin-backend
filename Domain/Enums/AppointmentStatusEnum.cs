namespace Domain.Enums
{
    public enum AppointmentStatusEnum : short
    {
        PENDING = 1,
        CONFIRMED = 2,
        IN_PROGRESS = 3,
        COMPLETED = 4,
        CANCELLED = 5,
        NO_SHOW = 6
    }
}