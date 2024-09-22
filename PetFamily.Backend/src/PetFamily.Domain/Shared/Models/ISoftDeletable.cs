namespace PetFamily.Domain.Shared.Models
{
    public interface ISoftDeletable
    {
        public bool IsDeleted();
        public void Delete();
        public void Restore();
    }
}
