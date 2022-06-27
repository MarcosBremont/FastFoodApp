using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using FastFoodApp.Configuracion;
using FastFoodApp.Entidad;
using FastFoodApp.Metodos;
using FastFoodApp.Modelo;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static FastFoodApp.Configuracion.Herramientas;

namespace FastFoodApp.Pantallas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrincipalPage : ContentPage
    {
        ModalCantidad modalCantidad = new ModalCantidad();
        ModalHacerPedido modalHacerPedido = new ModalHacerPedido();
        ToastConfigClass toastConfig = new ToastConfigClass();
        Herramientas herramientas = new Herramientas();

        MediaFile mediaFile;
        MediaFile mediaFileEmpresa;
        private bool busy;
        private object fileSelectedPath;
        public PrincipalPage()
        {
            InitializeComponent();
            btnMenu.Source = "hamburgerSodaAmarillo.png";

            _ = LlenarMenu();
            _ = LlenarNotificaciones();
            LlenarMiPerfil();
            _ = LlenarCarrito();

            ImgAgregarFoto.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    EscogerFoto();
                }),
                NumberOfTapsRequired = 1
            });

            gridPedidos.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {

                    StackLayoutPaginaPrincipal.IsVisible = false;
                    StackLayoutPedidos.IsVisible = true;
                    StackLayoutMiPerfil.IsVisible = false;
                    StackLayoutTuCarrito.IsVisible = false;
                    StackLayoutNotificaciones.IsVisible = false;
                    lsv_pedidos_Refreshing(null, null);
                    btnPedidos.Source = "TimeAmarillo";
                    btnMenu.Source = "hamburgerSodaWhite.png";
                    btnMiPerfil.Source = "userBlanco";
                    btnImgCarrito.Source = "MiCarritoBlanco";
                    btnNotitifaciones.Source = "bellWhite";
                }),
                NumberOfTapsRequired = 1
            });


            gridInicio.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    StackLayoutPaginaPrincipal.IsVisible = true;
                    StackLayoutMiPerfil.IsVisible = false;
                    StackLayoutTuCarrito.IsVisible = false;
                    StackLayoutNotificaciones.IsVisible = false;
                    lsv_menu_Refreshing(null, null);
                    StackLayoutPedidos.IsVisible = false;
                    btnPedidos.Source = "TimeBlanco";
                    btnMenu.Source = "hamburgerSodaAmarillo.png";
                    btnMiPerfil.Source = "userBlanco";
                    btnImgCarrito.Source = "MiCarritoBlanco";
                    btnNotitifaciones.Source = "bellWhite";


                }),
                NumberOfTapsRequired = 1
            });

            gridNotificaciones.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    StackLayoutPaginaPrincipal.IsVisible = false;
                    StackLayoutMiPerfil.IsVisible = false;
                    StackLayoutTuCarrito.IsVisible = false;
                    StackLayoutPedidos.IsVisible = false;
                    StackLayoutNotificaciones.IsVisible = true;
                    lsv_notificaciones_Refreshing(null, null);
                    btnPedidos.Source = "TimeBlanco";
                    btnMenu.Source = "hamburgerSodaWhite.png";
                    btnMiPerfil.Source = "userBlanco";
                    btnImgCarrito.Source = "MiCarritoBlanco";
                    btnNotitifaciones.Source = "bellAmarillo";

                }),
                NumberOfTapsRequired = 1
            });

            gridMiPerfil.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    StackLayoutPaginaPrincipal.IsVisible = false;
                    StackLayoutPedidos.IsVisible = false;
                    StackLayoutTuCarrito.IsVisible = false;
                    StackLayoutNotificaciones.IsVisible = false;

                    StackLayoutMiPerfil.IsVisible = true;
                    btnPedidos.Source = "TimeBlanco";
                    btnMenu.Source = "hamburgerSodaWhite.png";
                    btnMiPerfil.Source = "userAmarillo";
                    btnImgCarrito.Source = "MiCarritoBlanco";
                    btnNotitifaciones.Source = "bellWhite";


                }),
                NumberOfTapsRequired = 1
            });


            gridMiCarrito.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    StackLayoutPaginaPrincipal.IsVisible = false;
                    StackLayoutPedidos.IsVisible = false;
                    StackLayoutMiPerfil.IsVisible = false;
                    StackLayoutTuCarrito.IsVisible = true;
                    StackLayoutNotificaciones.IsVisible = false;
                    lsv_Carrito_Refreshing(null, null);

                    btnPedidos.Source = "TimeBlanco";
                    btnMenu.Source = "hamburgerSodaWhite.png";
                    btnMiPerfil.Source = "userBlanco";
                    btnImgCarrito.Source = "MiCarritoAmarillo";
                    btnNotitifaciones.Source = "bellWhite";


                }),
                NumberOfTapsRequired = 1
            });
        }

        private async void LlenarMiPerfil()
        {

            FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
            var datos = await metodos.IniciarSesion(App.correo, App.clave);


            TxtNombre.Text = datos.nombre.ToUpper();
            TxtApellido.Text = datos.apellido.ToUpper();
            TxtDireccion.Text = datos.direccion.ToUpper();
            TxtTelefono.Text = datos.telefono.ToUpper();
            TxtClave.Text = datos.clave.ToUpper();
            TxtEmail.Text = datos.correo.ToUpper();
            PerfilFoto.Source = datos.foto;

        }

        [Obsolete]
        public async Task<bool> RequestThisPermision(Permiso permiso)
        {
            if (busy)
                return false;

            busy = true;

            var status = Plugin.Permissions.Abstractions.PermissionStatus.Unknown;
            switch (permiso)
            {
                case Permiso.Camara:
                    status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                    break;
                case Permiso.Galeria:
                    status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Photos);
                    break;
            }

            if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
            {
                switch (permiso)
                {
                    case Permiso.Camara:
                        status = await Herramientas.CheckPermissions(Permission.Camera);
                        break;
                    case Permiso.Galeria:
                        status = await Herramientas.CheckPermissions(Permission.Photos);
                        break;
                }
            }

            busy = false;
            return status == Plugin.Permissions.Abstractions.PermissionStatus.Granted ? true : false;
        }



        private async void EscogerFoto()
        {


            try
            {

                fileSelectedPath = null;

                if (Device.RuntimePlatform == Device.Android)
                {
                    await RequestThisPermision(Permiso.Galeria);
                }
                else
                {
                    if (!await RequestThisPermision(Permiso.Galeria))
                    {
                        // MsgNormal(senderPage, "Permiso denegado, no se puede acceder a la galería");
                        return;
                    }
                }

                if (Device.RuntimePlatform == Device.Android)
                {


                    if (!CrossMedia.Current.IsPickPhotoSupported)
                    {
                        // MsgNormal(senderPage, "Este dispositivo no puede seleccionar imágenes", "OK");
                        return;
                    }
                    else
                    {
                        try
                        {

                            //Check for Media Library Permisions
                            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.MediaLibrary);

                            PickPicture:
                            if (status == Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                            {



                                var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                                {
                                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                                    MaxWidthHeight = 600,
                                    SaveMetaData = false
                                });
                                mediaFileEmpresa = file;


                                if (file == null)
                                    return;
                                else
                                {
                                    while (fileSelectedPath == null)
                                        fileSelectedPath = file.Path;

                                    PerfilFoto.Source = ImageSource.FromStream(() => file.GetStreamWithImageRotatedForExternalStorage());
                                    //if (mediaFileEmpresa != null)
                                    //{
                                    //    BtnAgregarAlMenu.IsVisible = true;
                                    //}
                                }
                            }
                            else
                            {
                                await CrossPermissions.Current.RequestPermissionsAsync(Plugin.Permissions.Abstractions.Permission.MediaLibrary);
                                var statusTwo = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.MediaLibrary);

                                if (status == Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                                    goto PickPicture;
                                else
                                    return;
                            }
                        }
                        catch (Exception ex)
                        {
                            _ = ex.Message;
                            //MsgNormal(senderPage, "Permiso denegado, no se puede acceder a la galería");
                            return;
                        }
                    }

                }
                else
                {
                    await CrossMedia.Current.Initialize();

                    var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                    var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                    var photoLibraryStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.MediaLibrary);

                    while (cameraStatus != Plugin.Permissions.Abstractions.PermissionStatus.Granted && storageStatus != Plugin.Permissions.Abstractions.PermissionStatus.Granted && photoLibraryStatus != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                    {
                        var cameraStatus1 = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);
                        var storageStatus1 = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
                        var photoLibrary = await CrossPermissions.Current.RequestPermissionsAsync(Permission.MediaLibrary);

                        cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                        storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                        photoLibraryStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.MediaLibrary);
                    }

                    if (!CrossMedia.Current.IsPickPhotoSupported)
                    {
                        //MsgNormal("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                        return;
                    }
                    var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                    {
                        PhotoSize = PhotoSize.MaxWidthHeight,
                        MaxWidthHeight = 2000
                    });

                    mediaFileEmpresa = file;

                    if (file == null)
                        return;

                    PerfilFoto.Source = ImageSource.FromStream(() =>
                    {

                        while (fileSelectedPath == null)
                            fileSelectedPath = file.Path;

                        var stream = file.GetStream();
                        return stream;
                    });

                    //if (mediaFileEmpresa != null)
                    //{
                    //    BtnAgregarAlMenu.IsVisible = true;
                    //}


                }
            }
            catch (Exception)
            {
                return;
            }

        }


        public async Task LlenarMenu()
        {
            try
            {
                lsv_menu.IsVisible = false;
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                lsv_menu.ItemsSource = null;
                var datos = await metodos.ObtenerMenu("S");
                lsv_menu.ItemsSource = datos;
                lsv_menu.IsVisible = true;


            }
            catch (Exception ex)
            {
                toastConfig.MostrarNotificacion($"No se pudo establecer la conexión, por favor intente nuevamente.", ToastPosition.Top, 4, "#e63946");
            }
        }

        public async Task LlenarNotificaciones()
        {
            try
            {
                lsv_notificaciones.IsVisible = false;
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                lsv_notificaciones.ItemsSource = null;
                var datos = await metodos.ObtenerNotificaciones(0, "S");
                if (datos.Count == 0)
                {
                    LblNoHayOfertasParaElDiaDeHoy.IsVisible = true;
                }
                else
                {
                    LblNoHayOfertasParaElDiaDeHoy.IsVisible = false;
                }
                lsv_notificaciones.ItemsSource = datos;
                lsv_notificaciones.IsVisible = true;


            }
            catch (Exception ex)
            {
                toastConfig.MostrarNotificacion($"No se pudo establecer la conexión, por favor intente nuevamente.", ToastPosition.Top, 4, "#e63946");
            }
        }

        public async Task LlenarCarrito()
        {
            try
            {
                lsv_Carrito.IsVisible = false;
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                lsv_Carrito.ItemsSource = null;
                var datos = await metodos.ObtenerCarritoPorUsuario(App.NumeroOrdenGeneral, App.idusuarios, "PRE-ORDEN");
                if (datos.Count == 0)
                {
                    LblAunNoAgregasNada.IsVisible = true;
                    BtnHacerPedido.IsVisible = false;
                    ImgCarEmpty.IsVisible = true;
                }
                else
                {
                    LblAunNoAgregasNada.IsVisible = false;
                    BtnHacerPedido.IsVisible = true;
                    ImgCarEmpty.IsVisible = false;


                }
                lsv_Carrito.ItemsSource = datos;

                lsv_Carrito.IsVisible = true;
                App.TodalPrecioCarrito = datos.Sum(n => n.total_por_producto);

            }
            catch (Exception ex)
            {
                toastConfig.MostrarNotificacion($"No se pudo establecer la conexión, por favor intente nuevamente.", ToastPosition.Top, 4, "#e63946");

            }
        }


        public async Task LlenarPedidos()
        {
            try
            {
                lsv_pedidos.IsVisible = false;
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                lsv_pedidos.ItemsSource = null;
                var datos = await metodos.ObtenerCarritoPorUsuario(0, App.idusuarios, "PENDIENTE");
                if (datos.Count == 0)
                {
                    LblAunNoHasPedidoNada.IsVisible = true;
            
                }
                else
                {
                    LblAunNoHasPedidoNada.IsVisible = false;
        
                }
                lsv_pedidos.ItemsSource = datos;
                lsv_pedidos.IsVisible = true;
            }
            catch (Exception ex)
            {
                toastConfig.MostrarNotificacion($"No se pudo establecer la conexión, por favor intente nuevamente.", ToastPosition.Top, 4, "#e63946");

            }
        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();

            try
            {
                lsv_menu.IsVisible = false;
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                lsv_menu.ItemsSource = null;
                var datos = await metodos.ObtenerMenu("S");
                lsv_menu.ItemsSource = datos;
                lsv_menu.IsVisible = true;

            }
            catch (Exception ex)
            {

            }


        }

        [Obsolete]
        async void BtnAgregar_Clicked(System.Object sender, System.EventArgs e)
        {

            var b = (Button)sender;

            var ob = b.CommandParameter as EMenu;

            if (ob != null)

            {

                // retrieve the value from the ‘ob’ and continue your work.

            }
            App.Precio = ob.precio;
            App.producto = ob.nombre;
            App.idProducto = ob.idmenu_fast_food;
            modalCantidad = new ModalCantidad();
            modalCantidad.Disappearing += ModalCantidad_Disappearing;
            await PopupNavigation.PushAsync(modalCantidad);

        }

        private void ModalCantidad_Disappearing(object sender, EventArgs e)
        {
            App.producto = "";
        }

        void lsv_menu_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var product = (EMenu)e.SelectedItem;
            App.Precio = product.precio;
        }

        [Obsolete]
        private async void BtnHacerPedido_Clicked(object sender, EventArgs e)
        {
            modalHacerPedido = new ModalHacerPedido();
            modalHacerPedido.Disappearing += ModalHacerPedido_Disappearing;
            await PopupNavigation.PushAsync(modalHacerPedido);

        }

        private void ModalHacerPedido_Disappearing(object sender, EventArgs e)
        {
            lsv_Carrito_Refreshing(null, null);

        }

        private async void BtnCerrarSesion_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Información", "¿Desea cerrar sesión?", "SI", "NO"))
            {
                //DatosConfiguracion.GrabarDatosSesion("", "", "S");
                LoginPage loginPage = new LoginPage();
                await Navigation.PushModalAsync(loginPage);
                //DatosConfiguracion.EliminarDatosSesion();


            }
        }

        void btnEditarPerfil_Clicked(System.Object sender, System.EventArgs e)
        {
            TxtEmail.IsEnabled = true;
            TxtDireccion.IsEnabled = true;
            TxtTelefono.IsEnabled = true;
            TxtClave.IsEnabled = true;
            TxtNombre.IsEnabled = true;
            TxtApellido.IsEnabled = true;
            btnGuardarCambios.IsVisible = true;
            btnEditarPerfil.IsVisible = false;
            ImgAgregarFoto.IsVisible = true;

        }

        private void eyebuttonnegro_Clicked(object sender, EventArgs e)
        {
            eyebuttonnegro.IsVisible = false;
            eyebuttonazul.IsVisible = true;
            TxtClave.IsPassword = false;
        }

        private void eyebuttonazul_Clicked(object sender, EventArgs e)
        {
            eyebuttonnegro.IsVisible = true;
            eyebuttonazul.IsVisible = false;
            TxtClave.IsPassword = true;
        }

        public async void ActualizarUsuario(string nombre, string apellido, string direccion, string telefono, string email, string clave, int idusuarios)
        {
            try
            {
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                var datos = await metodos.ActualizarUsuario(nombre, apellido, direccion, telefono, email, clave, idusuarios);

            }
            catch (Exception ex)
            {

            }
        }

        async private void GrabarImageApiUsuarios(Stream st)
        {
            try
            {
                Acr.UserDialogs.UserDialogs.Instance.ShowLoading();
                var buffer = new byte[st.Length];
                st.Seek(0, SeekOrigin.Begin);
                st.Read(buffer, 0, buffer.Length);
                var base64 = Convert.ToBase64String(buffer);
                var result = await herramientas.SetPost<EUsuario>("FastFood/GrabarImagenCliente", new EUsuario() { idusuarios = App.idusuarios, foto = base64 });

                if (result.result == "OK")
                {
                    App.Foto = result.foto;
                    LlenarMiPerfil();
                    toastConfig.MostrarNotificacion($"Datos actualizados con exito", ToastPosition.Top, 3, "#4bbd62");
                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();

                }
                else
                {
                    toastConfig.MostrarNotificacion($"No se pudo actualizar la foto de perfil. Intente mas tarde.", ToastPosition.Top, 3, "#c82333");
                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();

                }
            }
            catch (Exception ex)
            {
                toastConfig.MostrarNotificacion($"No se pudo actualizar la foto del producto. Revise la conexión a internet.", ToastPosition.Top, 3, "#c82333");
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();

            }
            finally
            {
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
            }

        }

        void btnGuardarCambios_Clicked(System.Object sender, System.EventArgs e)
        {
            TxtEmail.IsEnabled = false;
            TxtDireccion.IsEnabled = false;
            TxtNombre.IsEnabled = false;
            TxtApellido.IsEnabled = false;
            TxtTelefono.IsEnabled = false;
            btnEditarPerfil.IsVisible = true;
            TxtClave.IsEnabled = false;
            btnGuardarCambios.IsVisible = false;
            ImgAgregarFoto.IsVisible = false;


            if (mediaFileEmpresa != null)
            {
                GrabarImageApiUsuarios(mediaFileEmpresa.GetStreamWithImageRotatedForExternalStorage());
            }

            ActualizarUsuario(TxtNombre.Text, TxtApellido.Text, TxtDireccion.Text, TxtTelefono.Text, TxtEmail.Text, TxtClave.Text, App.idusuarios);

        }

        async void lsv_menu_Refreshing(System.Object sender, System.EventArgs e)
        {
            FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
            lsv_menu.ItemsSource = null;
            var datos = await metodos.ObtenerMenu("S");
            lsv_menu.ItemsSource = datos;
            lsv_menu.IsRefreshing = false;

        }

        void lsv_pedidos_Refreshing(System.Object sender, System.EventArgs e)
        {
            _ = LlenarPedidos();
            lsv_pedidos.IsRefreshing = false;

        }

        void lsv_Carrito_Refreshing(System.Object sender, System.EventArgs e)
        {
            _ = LlenarCarrito();
            lsv_Carrito.IsRefreshing = false;
        }

        void lsv_notificaciones_Refreshing(System.Object sender, System.EventArgs e)
        {
            _ = LlenarNotificaciones();
            lsv_notificaciones.IsRefreshing = false;
        }

        private async void lsv_pedidos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var pedido = (EPedidos)e.SelectedItem;
            if (pedido.estado_del_pedido == "PENDIENTE")
            {
                if (await DisplayAlert("Información", "¿Desea cancelar su pedido?", "SI", "NO"))
                {

                }
            }
        }
    }
}