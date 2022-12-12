namespace SimpleStockMonitorPWA.App;

public partial class MainLayout
{
    bool _isDrawerOpen;
    void DrawerToggle()
    {
        _isDrawerOpen = !_isDrawerOpen;
    }
}