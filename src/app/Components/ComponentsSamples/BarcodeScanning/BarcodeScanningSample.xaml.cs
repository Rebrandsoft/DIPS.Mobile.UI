using DIPS.Mobile.UI.API.Camera.BarcodeScanning;

namespace Components.ComponentsSamples.BarcodeScanning;

public partial class BarcodeScanningSample
{
    private readonly BarcodeScanner m_barcodeScanner;
    private BarcodeScanningResultBottomSheet? m_barCodeResultBottomSheet;

    public BarcodeScanningSample()
    {
        InitializeComponent();
        m_barcodeScanner = new BarcodeScanner();
    }

    private async Task Start()
    {
        try
        {
            await m_barcodeScanner.Start(Preview, DidFindBarcode);
        }
        catch (Exception exception)
        {
            await Application.Current?.MainPage?.DisplayAlert("Failed, check console!", exception.Message, "Ok")!;
            Console.WriteLine(exception);
        }
    }

    private void DidFindBarcode(Barcode barcode)
    {
        m_barCodeResultBottomSheet = new BarcodeScanningResultBottomSheet();
        m_barCodeResultBottomSheet.Closed += BottomSheetClosed;
        m_barcodeScanner.Stop();
        m_barCodeResultBottomSheet.OpenWithBarCode(barcode);
    }

    private async void BottomSheetClosed(object? sender, EventArgs e)
    {
        _ = Start();
        if (m_barCodeResultBottomSheet != null)
        {
            m_barCodeResultBottomSheet.Closed -= BottomSheetClosed;
        }

        m_barCodeResultBottomSheet = null;
    }

    protected override void OnAppearing()
    {
        _ = Start();
        base.OnAppearing();
    }

    protected override void OnDisappearing()
    {
        m_barcodeScanner.Stop();
        base.OnDisappearing();
    }

    public void OnSleep()
    {
        m_barcodeScanner.Stop();
    }

    public void OnResume()
    {
        _ = Start();
    }
}