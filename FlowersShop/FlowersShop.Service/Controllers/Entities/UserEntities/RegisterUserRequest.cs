using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace FlowersShop.Service.Controllers.Entities.UserEntities;

 public class RegisterUserRequest : IValidatableObject
    {
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Patronymic { get; set; }
        public int RoleId { get; set; }
        public string ExternalId { get; set; } // Новое поле

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            // Валидация Email
            if (!Regex.IsMatch(Email, @"\w+@\w+[.]\w+"))
                errors.Add(new ValidationResult("Invalid email"));

            // Валидация имени (Name)
            if (!Regex.IsMatch(Name, @"[A-Z][a-z]+"))
                errors.Add(new ValidationResult("Name contains invalid symbols"));

            // Валидация фамилии (Surname)
            if (!Regex.IsMatch(Surname, @"[A-Z][a-z]+"))
                errors.Add(new ValidationResult("Surname contains invalid symbols"));

            // Валидация отчества (Patronymic)
            if (Patronymic != null && !Regex.IsMatch(Patronymic, @"[A-Z][a-z]+"))
                errors.Add(new ValidationResult("Patronymic contains invalid symbols"));

            // Валидация ExternalId
            if (string.IsNullOrEmpty(ExternalId))
                errors.Add(new ValidationResult("ExternalId cannot be empty"));

            // Дополнительные проверки
            if (PasswordHash.Length < 8)
                errors.Add(new ValidationResult("Password must be at least 8 characters long"));

            if (RoleId <= 0)
                errors.Add(new ValidationResult("RoleId must be greater than 0"));

            return errors;
        }

        public ValidationResult Validate2(ValidationContext validationContext)
        {
            var errorMessages = new StringBuilder();
            var errorFiledNames = new List<string>();

            // Валидация Email
            if (!Regex.IsMatch(Email, @"\w+@\w+[.]\w+"))
            {
                errorFiledNames.Add("Email");
                errorMessages.AppendLine("Invalid email");
            }

            // Валидация имени (Name)
            if (!Regex.IsMatch(Name, @"[A-Z][a-z]+"))
            {
                errorFiledNames.Add("Name");
                errorMessages.AppendLine("Name contains invalid symbols");
            }

            // Валидация фамилии (Surname)
            if (!Regex.IsMatch(Surname, @"[A-Z][a-z]+"))
            {
                errorFiledNames.Add("Surname");
                errorMessages.AppendLine("Surname contains invalid symbols");
            }

            // Валидация отчества (Patronymic)
            if (Patronymic != null && !Regex.IsMatch(Patronymic, @"[A-Z][a-z]+"))
            {
                errorFiledNames.Add("Patronymic");
                errorMessages.AppendLine("Patronymic contains invalid symbols");
            }

            // Валидация ExternalId
            if (string.IsNullOrEmpty(ExternalId))
            {
                errorFiledNames.Add("ExternalId");
                errorMessages.AppendLine("ExternalId cannot be empty");
            }

            // Проверка пароля
            if (PasswordHash.Length < 8)
            {
                errorFiledNames.Add("PasswordHash");
                errorMessages.AppendLine("Password must be at least 8 characters long");
            }

            // Проверка RoleId
            if (RoleId <= 0)
            {
                errorFiledNames.Add("RoleId");
                errorMessages.AppendLine("RoleId must be greater than 0");
            }

            var errors = new ValidationResult(errorMessages.ToString(), errorFiledNames);
            return errors;
        }
    }
    