using PurchasingSystem.Domain.Shared.SeedWorks;
using PurchasingSystem.Domain.User.Enums;
using PurchasingSystem.Domain.User.Events;
using PurchasingSystem.Domain.User.ValueObjects;

namespace PurchasingSystem.Domain.User.Entities
{
    public class User : Entity
    {
        private User() : base(Guid.NewGuid()) { }
        private User(
            FullName fullName,
            Email email,
            Password password,
            Cpf cpf,
            Guid id) : base(id) { }

        public static User Create(string name, string lastName, string email, string password, string cpf)
        {
            var id = Guid.NewGuid();
            var fullNameVO = FullName.Create(name, lastName);
            var emailVO = Email.Create(email);
            var cpfVO = Cpf.Create(cpf);
            var passwordVO = Password.Create(password);
            var user = new User(fullNameVO, emailVO, passwordVO, cpfVO, id);

            user.RaiseDomainEvents(new CreatedUserEvent(user.Id));
            return user;
        }

        public void Update(string name, string lastName, string email, string password)
        {
            FullName = FullName.Create(name, lastName);
            Email = Email.Create(email);
            Password = Password.Create(password);
        }

        public void Delete()
        {
            RaiseDomainEvents(new DeletedUserEvent(Id));
        }

        public FullName FullName { get; private set; }
        public Email Email { get; private set; }
        public Password Password { get; private set; }
        public Cpf Cpf { get; private set; }
        public Roles Role { get; private set; } = Roles.User;
    }
}
