namespace HoldTheAllergen.Data.Models
{
    public interface IFormHandler<in T>
    {
        void Handle(T form);
    }
}