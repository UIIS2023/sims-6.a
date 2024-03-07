namespace InitialProject.Services.IServices
{
    using InitialProject.Domen.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IVoucherService
    {
        public List<Voucher> GetAllForUser(int userId);
        public void Delete(Voucher voucher);

        public void CreateVoucher(int userId);
    }
}
