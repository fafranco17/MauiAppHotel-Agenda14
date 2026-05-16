using MauiAppHotel.Models;

namespace MauiAppHotel;

public partial class ContratacaoHospedagem : ContentPage
{
    App PropriedadesApp;

    public ContratacaoHospedagem()
    {
        InitializeComponent();

        PropriedadesApp = (App)Application.Current;
        pck_quarto.ItemsSource = PropriedadesApp.lista_quartos;

        dtpck_checkin.MinimumDate = DateTime.Now;
        dtpck_checkin.MaximumDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, DateTime.Now.Day);

        
        DateTime data_checkin = dtpck_checkin.Date ?? DateTime.Now;
        dtpck_checkout.MinimumDate = data_checkin.AddDays(1);
        dtpck_checkout.MaximumDate = data_checkin.AddMonths(6);
    }

    private void dtpck_checkin_DateSelected(object sender, DateChangedEventArgs e)
    {
        DatePicker elemento = sender as DatePicker;

       
        DateTime data_selecionada_checkin = elemento.Date ?? DateTime.Now;

        dtpck_checkout.MinimumDate = data_selecionada_checkin.AddDays(1);
        dtpck_checkout.MaximumDate = data_selecionada_checkin.AddMonths(6);
    }

    private async void BtnAvancar_Clicked(object sender, EventArgs e)
    {
        try
        {
            
            if (pck_quarto.SelectedItem == null)
                throw new Exception("Por favor, selecione uma suíte.");

            
            Hospedagem h = new Hospedagem
            {
                QuartoSelecionado = (Quarto)pck_quarto.SelectedItem,
                QtdAdultos = (int)stp_adultos.Value,
                QtdCriancas = (int)stp_criancas.Value,

                
                DataCheckIn = dtpck_checkin.Date ?? DateTime.Now,
                DataCheckOut = dtpck_checkout.Date ?? DateTime.Now.AddDays(1)
            };

            
            await Navigation.PushAsync(new HospedagemContratada()
            {
                BindingContext = h
            });
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private async void BtnSobre_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Sobre());
    }
}