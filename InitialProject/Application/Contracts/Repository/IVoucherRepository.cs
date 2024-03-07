using InitialProject.Domen.Model;
using System.Collections.Generic;

namespace InitialProject.Aplication.Contracts.Repository
{
    public interface IVoucherRepository
    {
        void CheckValidity();
        void Delete(Voucher voucher);
        List<Voucher> GetAll();
        int NextId();
        Voucher Save(Voucher voucher);

        void GiveLoyalityVoucher(int userId);
    }
}