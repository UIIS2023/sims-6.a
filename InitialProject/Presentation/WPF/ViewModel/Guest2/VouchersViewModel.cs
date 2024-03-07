using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Presentation.WPF.ViewModel.Guest2
{
    public class VouchersViewModel
    {
        public ObservableCollection<Voucher> Vouchers { get; set; }
        private readonly IVoucherService _voucherService;
        public VouchersViewModel(int userId)
        {
            _voucherService = Injector.CreateInstance<IVoucherService>();
            Vouchers = new ObservableCollection<Voucher>(_voucherService.GetAllForUser(userId));
        }
    }
}
