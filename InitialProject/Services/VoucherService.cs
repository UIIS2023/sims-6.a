namespace InitialProject.Services
{
    using InitialProject.Aplication.Contracts.Repository;
    using InitialProject.Aplication.Factory;
    using InitialProject.Domen.Model;
    using InitialProject.Repository;
    using InitialProject.Services.IServices;
    using System.Collections.Generic;
    using System.Linq;

    public class VoucherService : IVoucherService
    {
        private readonly IVoucherRepository _repository;
        public VoucherService()
        {
            _repository = Injector.CreateInstance<IVoucherRepository>();
        }

        public List<Voucher> GetAllForUser(int userId)
        {
            return _repository.GetAll().Where(v => v.UserId == userId).ToList();
        }

        public void Delete(Voucher voucher)
        {
            _repository.Delete(voucher);
        }

        public void CreateVoucher(int userId)
        {
            _repository.GiveLoyalityVoucher(userId);
        }
    }
}
