using CognitiveMachineBaby.ViewModels;
using Xamarin.Forms;

namespace CognitiveMachineBaby
{
    public partial class CognitiveMachineBabyPage : ContentPage
    {
        public CognitiveMachineBabyPage()
        {
            InitializeComponent();

			BindingContext = new CognitiveMachineBabyViewModel();
        }
    }
}
