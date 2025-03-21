using Indotalent.Applications.CustomerContacts;
using Indotalent.Applications.Customers;
using Indotalent.Applications.NumberSequences;
using Indotalent.Domain.Entities;

namespace Indotalent.Data.Demo
{
    public static class DemoCustomerContact
    {
        public static async Task GenerateAsync(IServiceProvider services)
        {
            var customerContactService = services.GetRequiredService<CustomerContactService>();
            var numberSequenceService = services.GetRequiredService<NumberSequenceService>();
            var customerService = services.GetRequiredService<CustomerService>();

            var firsts = new string[]
            {
                "Adam", "Sarah", "Michael", "Emily", "David", "Jessica", "Kevin", "Samantha", "Jason", "Olivia",
                "Matthew", "Ashley", "Christopher", "Jennifer", "Nicholas", "Amanda", "Alexander", "Stephanie",
                "Jonathan", "Lauren"
            };

            var lasts = new string[]
            {
                "Johnson", "Williams", "Brown", "Jones", "Miller", "Davis", "Garcia", "Rodriguez", "Wilson",
                "Martinez", "Anderson", "Taylor", "Thomas", "Hernandez", "Moore", "Martin", "Jackson", "Thompson",
                "White", "Lopez"
            };

            var jobTitles = new string[]
            {
                "Director Ejecutivo", "Científico de Datos", "Gerente de Producto",
                "Ejecutivo de Desarrollo de Negocios", "Consultor de TI", "Especialista en Redes Sociales",
                "Analista de Investigación", "Redactor de Contenidos", "Gerente de Operaciones",
                "Planificador Financiero", "Desarrollador de Software", "Gerente de Éxito del Cliente",
                "Coordinador de Marketing", "Tester de Control de Calidad", "Especialista en Recursos Humanos",
                "Coordinador de Eventos", "Ejecutivo de Cuentas", "Administrador de Redes", "Gerente de Ventas",
                "Asistente Legal"
            };

            var customers = customerService.GetAll().Select(x => x.Id).ToArray();

            Random random = new Random();

            foreach (var item in customers)
            {
                for (int i = 0; i < 3; i++)
                {
                    var first = DbInitializer.GetRandomString(firsts, random);
                    var last = DbInitializer.GetRandomString(lasts, random);

                    await customerContactService.AddAsync(new CustomerContact
                    {
                        Name = $"{first} {last}",
                        Number = numberSequenceService.GenerateNumber(nameof(CustomerContact), "", "CC"),
                        CustomerId = item,
                        JobTitle = DbInitializer.GetRandomString(jobTitles, random),
                        EmailAddress = $"{first.ToLower()}.{last.ToLower()}@gmail.com"
                    });
                }
            }
        }
    }
}
