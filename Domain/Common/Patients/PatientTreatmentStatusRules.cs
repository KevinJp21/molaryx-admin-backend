using Domain.Enums;

namespace Domain.Common.Patients
{
    public static class PatientTreatmentStatusRules
    {
        private static readonly HashSet<PatientTreatmentStatusEnum> BlockingStatuses =
        [
            PatientTreatmentStatusEnum.ACTIVE,
            PatientTreatmentStatusEnum.PAUSED,
            PatientTreatmentStatusEnum.COMPLETED,
            PatientTreatmentStatusEnum.CANCELLED
        ];

        private static readonly Dictionary<PatientTreatmentStatusEnum, HashSet<PatientTreatmentStatusEnum>> AllowedTransitions =
            new()
            {
                [PatientTreatmentStatusEnum.ACTIVE] =
                [
                    PatientTreatmentStatusEnum.PAUSED,
                    PatientTreatmentStatusEnum.COMPLETED,
                    PatientTreatmentStatusEnum.CANCELLED
                ],
                [PatientTreatmentStatusEnum.PAUSED] =
                [
                    PatientTreatmentStatusEnum.ACTIVE,
                    PatientTreatmentStatusEnum.CANCELLED
                ],
                [PatientTreatmentStatusEnum.COMPLETED] = [],
                [PatientTreatmentStatusEnum.CANCELLED] = [],
            };

        private static readonly Dictionary<PatientTreatmentStatusEnum, string> DisplayNames = new()
        {
            [PatientTreatmentStatusEnum.ACTIVE] = "Activo",
            [PatientTreatmentStatusEnum.PAUSED] = "Pausado",
            [PatientTreatmentStatusEnum.COMPLETED] = "Completado",
            [PatientTreatmentStatusEnum.CANCELLED] = "Cancelado"
        };

        public static bool IsBlocking(short idPatientTreatmentStatus)
            => IsBlocking((PatientTreatmentStatusEnum)idPatientTreatmentStatus);

        public static bool IsBlocking(PatientTreatmentStatusEnum status)
            => BlockingStatuses.Contains(status);

        public static string GetDisplayName(PatientTreatmentStatusEnum status)
            => DisplayNames.TryGetValue(status, out var name) ? name : status.ToString();

        public static bool IsFinal(short idPatientTreatmentStatus)
            => IsFinal((PatientTreatmentStatusEnum)idPatientTreatmentStatus);

        public static bool IsFinal(PatientTreatmentStatusEnum status)
            => status is PatientTreatmentStatusEnum.COMPLETED
                or PatientTreatmentStatusEnum.CANCELLED;

        public static void EnsureCanEdit(short idPatientTreatmentStatus)
            => EnsureCanEdit((PatientTreatmentStatusEnum)idPatientTreatmentStatus);

        public static void EnsureCanEdit(PatientTreatmentStatusEnum status)
        {
            if (IsFinal(status))
            {
                throw new InvalidOperationException(
                    $"No se puede editar un tratamiento en estado {GetDisplayName(status)}.");
            }
        }

        public static bool CanReceivePayment(short idPatientTreatmentStatus)
            => CanReceivePayment((PatientTreatmentStatusEnum)idPatientTreatmentStatus);

        public static bool CanReceivePayment(PatientTreatmentStatusEnum status)
            => status is not PatientTreatmentStatusEnum.CANCELLED;

        public static void EnsureCanReceivePayment(short idPatientTreatmentStatus)
            => EnsureCanReceivePayment((PatientTreatmentStatusEnum)idPatientTreatmentStatus);

        public static void EnsureCanReceivePayment(PatientTreatmentStatusEnum status)
        {
            if (!Enum.IsDefined(status))
            {
                throw new InvalidOperationException("El estado del tratamiento no es válido.");
            }

            if (!CanReceivePayment(status))
            {
                throw new InvalidOperationException(
                    $"No se puede registrar un pago en un tratamiento en estado {GetDisplayName(status)}.");
            }
        }

        public static bool CanLinkAppointment(short idPatientTreatmentStatus)
            => CanLinkAppointment((PatientTreatmentStatusEnum)idPatientTreatmentStatus);

        public static bool CanLinkAppointment(PatientTreatmentStatusEnum status)
            => status is PatientTreatmentStatusEnum.ACTIVE;

        public static void EnsureCanLinkAppointment(short idPatientTreatmentStatus)
            => EnsureCanLinkAppointment((PatientTreatmentStatusEnum)idPatientTreatmentStatus);

        public static void EnsureCanLinkAppointment(PatientTreatmentStatusEnum status)
        {
            if (!Enum.IsDefined(status))
            {
                throw new InvalidOperationException("El estado del tratamiento no es válido.");
            }

            if (!CanLinkAppointment(status))
            {
                throw new InvalidOperationException(
                    $"No se puede asociar una cita a un tratamiento en estado {GetDisplayName(status)}.");
            }
        }

        public static void EnsureCanTransition(short fromStatus, short toStatus)
            => EnsureCanTransition(
                (PatientTreatmentStatusEnum)fromStatus,
                (PatientTreatmentStatusEnum)toStatus);

        public static void EnsureCanTransition(
            PatientTreatmentStatusEnum from,
            PatientTreatmentStatusEnum to)
        {
            if (from == to)
            {
                return;
            }

            if (!Enum.IsDefined(from) || !Enum.IsDefined(to))
            {
                throw new InvalidOperationException("El estado del tratamiento no es válido.");
            }

            if (!AllowedTransitions.TryGetValue(from, out var allowed) || !allowed.Contains(to))
            {
                throw new InvalidOperationException(
                    $"No se puede cambiar el tratamiento de {GetDisplayName(from)} a {GetDisplayName(to)}.");
            }
        }
    }
}
