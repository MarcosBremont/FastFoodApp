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
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static FastFoodApp.Configuracion.Herramientas;

namespace FastFoodApp.Pantallas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrincipalPageEmpresa : ContentPage
    {
        ModalCantidad modalCantidad = new ModalCantidad();
        ModalMenu modalMenu = new ModalMenu();
        ModalHacerPedido modalHacerPedido = new ModalHacerPedido();
        ModalCambiarProgreso modalCambiarProgreso = new ModalCambiarProgreso();
        ToastConfigClass toastConfig = new ToastConfigClass();
        Herramientas herramientas = new Herramientas();

        List<ENotificaciones> ListENotificaciones = new List<ENotificaciones>();
        int id = 0;
        string Picker = "";
        private bool busy;
        MediaFile mediaFile;

        private object fileSelectedPath;
        public PrincipalPageEmpresa()
        {
            InitializeComponent();
            btnMenuEmpresa.Source = "hamburgerSodaAmarillo.png";

            _ = LlenarMenu();

            gridPedidosEmpresa.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    StackLayoutPedidosEmpresa.IsVisible = true;
                    StackLayoutPaginaPrincipal.IsVisible = false;
                    StackLayoutMiPerfil.IsVisible = false;
                    StackLayoutTuCarrito.IsVisible = false;
                    StackLayoutNotificacionesEmpresa.IsVisible = false;


                    btnPedidosEmpresa.Source = "TimeAmarillo";
                    btnMenuEmpresa.Source = "hamburgerSodaWhite.png";
                    btnMiPerfil.Source = "userBlanco";
                    btnImgAnotarPedidos.Source = "MiCarritoBlanco";
                    btnNotitifacionesEmpresa.Source = "bellWhite";
                    _ = LlenarPedidos();
                }),
                NumberOfTapsRequired = 1
            });


            gridInicioEmpresa.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    StackLayoutPaginaPrincipal.IsVisible = true;
                    StackLayoutMiPerfil.IsVisible = false;
                    StackLayoutTuCarrito.IsVisible = false;
                    StackLayoutNotificacionesEmpresa.IsVisible = false;

                    StackLayoutPedidosEmpresa.IsVisible = false;
                    btnPedidosEmpresa.Source = "TimeBlanco";
                    btnMenuEmpresa.Source = "hamburgerSodaAmarillo.png";
                    btnMiPerfil.Source = "userBlanco";
                    btnImgAnotarPedidos.Source = "MiCarritoBlanco";
                    btnNotitifacionesEmpresa.Source = "bellWhite";


                }),
                NumberOfTapsRequired = 1
            });

            gridNotificacionesEmpresa.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    if (App.empresa == "S")
                    {
                        StackLayoutPaginaPrincipal.IsVisible = false;
                        StackLayoutMiPerfil.IsVisible = false;
                        StackLayoutTuCarrito.IsVisible = false;
                        StackLayoutPedidosEmpresa.IsVisible = false;
                        StackLayoutNotificacionesEmpresa.IsVisible = true;

                        _ = LlenarNotificaciones();
                    }
                    else
                    {
                        StackLayoutPaginaPrincipal.IsVisible = false;
                        StackLayoutMiPerfil.IsVisible = false;
                        StackLayoutTuCarrito.IsVisible = false;
                        StackLayoutPedidosEmpresa.IsVisible = false;
                        StackLayoutNotificacionesEmpresa.IsVisible = true;
                    }



                    btnPedidosEmpresa.Source = "TimeBlanco";
                    btnMenuEmpresa.Source = "hamburgerSodaWhite.png";
                    btnMiPerfil.Source = "userBlanco";
                    btnImgAnotarPedidos.Source = "MiCarritoBlanco";
                    btnNotitifacionesEmpresa.Source = "bellAmarillo";


                }),
                NumberOfTapsRequired = 1
            });

            gridNotificacionesEmpresa.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    StackLayoutPaginaPrincipal.IsVisible = false;
                    StackLayoutMiPerfil.IsVisible = false;
                    StackLayoutTuCarrito.IsVisible = false;
                    StackLayoutPedidosEmpresa.IsVisible = false;
                    StackLayoutNotificacionesEmpresa.IsVisible = true;

                    _ = LlenarNotificaciones();

                    btnPedidosEmpresa.Source = "TimeBlanco";
                    btnMenuEmpresa.Source = "hamburgerSodaWhite.png";
                    btnMiPerfil.Source = "userBlanco";
                    btnImgAnotarPedidos.Source = "MiCarritoBlanco";
                    btnNotitifacionesEmpresa.Source = "bellAmarillo";

                }),
                NumberOfTapsRequired = 1
            });

            gridMiPerfilEmpresa.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    StackLayoutPaginaPrincipal.IsVisible = false;
                    StackLayoutPedidosEmpresa.IsVisible = false;
                    StackLayoutTuCarrito.IsVisible = false;
                    StackLayoutNotificacionesEmpresa.IsVisible = false;

                    StackLayoutMiPerfil.IsVisible = true;
                    btnPedidosEmpresa.Source = "TimeBlanco";
                    btnMenuEmpresa.Source = "hamburgerSodaWhite.png";
                    btnMiPerfil.Source = "userAmarillo";
                    btnImgAnotarPedidos.Source = "MiCarritoBlanco";
                    btnNotitifacionesEmpresa.Source = "bellWhite";

                    LlenarMiPerfil();

                }),
                NumberOfTapsRequired = 1
            });


            gridAnotarPedidos.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    StackLayoutPaginaPrincipal.IsVisible = false;
                    StackLayoutPedidosEmpresa.IsVisible = false;
                    StackLayoutMiPerfil.IsVisible = false;
                    StackLayoutTuCarrito.IsVisible = true;
                    StackLayoutNotificacionesEmpresa.IsVisible = false;

                    btnPedidosEmpresa.Source = "TimeBlanco";
                    btnMenuEmpresa.Source = "hamburgerSodaWhite.png";
                    btnMiPerfil.Source = "userBlanco";
                    btnImgAnotarPedidos.Source = "MiCarritoAmarillo";
                    btnNotitifacionesEmpresa.Source = "bellWhite";

                    _ = LlenarCarrito();

                }),
                NumberOfTapsRequired = 1
            });
        }

        private void LlenarMiPerfil()
        {
            TxtNombre.Text = App.nombre;
            TxtApellido.Text = App.apellido;
            TxtDireccion.Text = App.direccion;
            TxtTelefono.Text = App.telefono;
            TxtClave.Text = App.clave;
            TxtEmail.Text = App.correo;

        }

        public async Task LlenarMenu()
        {
            try
            {
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                var datos = await metodos.ObtenerMenu();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task LlenarNotificaciones()
        {
            try
            {
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                var datos = await metodos.ObtenerNotificaciones(0, 0.ToString());
                lsv_notificaciones.ItemsSource = datos;

            }
            catch (Exception ex)
            {

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
                }
                else
                {
                    LblAunNoAgregasNada.IsVisible = false;

                }
                lsv_Carrito.ItemsSource = datos;

                lsv_Carrito.IsVisible = true;
                App.TodalPrecioCarrito = datos.Sum(n => n.total_por_producto);

            }
            catch (Exception ex)
            {

            }
        }


        public async Task LlenarPedidos()
        {
            try
            {
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                var datos = await metodos.ObtenerPedidos("PENDIENTE", 1);
                lsv_pedidosEmpresa.ItemsSource = datos;
            }
            catch (Exception ex)
            {

            }
        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();

            try
            {
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                var datos = await metodos.ObtenerMenu();
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
            btnGuardarCambios.IsVisible = true;
            btnEditarPerfil.IsVisible = false;
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

        void btnGuardarCambios_Clicked(System.Object sender, System.EventArgs e)
        {
            TxtEmail.IsEnabled = false;
            TxtDireccion.IsEnabled = false;
            TxtTelefono.IsEnabled = false;
            btnEditarPerfil.IsVisible = true;
            TxtClave.IsEnabled = false;
            btnGuardarCambios.IsVisible = false;

            ActualizarUsuario(TxtNombre.Text, TxtApellido.Text, TxtDireccion.Text, TxtTelefono.Text, TxtEmail.Text, TxtClave.Text, App.idusuarios);

        }

        void BtnAgregarAlMenu_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                AgregarProductoAlMenu(TxtNombreProducto.Text, Convert.ToInt32(TxtPrecio.Text), Picker, "0", TxtDescripcion.Text);
                toastConfig.MostrarNotificacion($"Producto agregado al menú", ToastPosition.Top, 3, "#51C560");
            }
            catch (Exception ex)
            {
                toastConfig.MostrarNotificacion($"Ocurrio un error, intenta nuevamente o comunicate con el administrador", ToastPosition.Top, 3, "#51C560");

            }

        }




        private async void AgregarProductoAlMenu(string nombre, int precio, string disponibilidad, string foto, string descripcion)
        {
            try
            {
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                var datos = await metodos.AgregarProductoAlMenu(nombre, precio, disponibilidad, foto, descripcion);
                id = datos.Id;
                GrabarImageApi(mediaFile.GetStreamWithImageRotatedForExternalStorage());
                TxtNombreProducto.Text = "";
                TxtPrecio.Text = "";
                TxtDescripcion.Text = "";
                imgProducto.Source = "DefaultFood.png";

            }
            catch (Exception ex)
            {

            }
        }

        public void SeleccionarOTomarFoto(Page senderPage, string titulo, string cancelButton, Image imgToWork)
        {

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    UserDialogs.Instance.ActionSheet(new ActionSheetConfig()
                                                     .SetTitle(titulo)
                                                     .Add("Tomar Foto", () => TakePhotoAsync(senderPage, imgToWork))
                                                     .Add("Seleccionar foto", () => PickPhotoAsync(senderPage, imgToWork)).SetCancel(cancelButton)
                                                     );
                    break;

                case Device.Android:
                    UserDialogs.Instance.ActionSheet(new ActionSheetConfig()
                                                     .SetTitle(titulo)
                                                     .Add("Tomar Foto", () => TakePhotoAsync(senderPage, imgToWork), "camera.png")
                                                     .Add("Seleccionar foto", () => PickPhotoAsync(senderPage, imgToWork), "gallery.png").SetCancel(cancelButton)
                                                     );
                    break;
            }

        }

        private async void PickPhotoAsync(Page senderPage, Image imgToWork)
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
                                    PhotoSize = PhotoSize.MaxWidthHeight,
                                    MaxWidthHeight = 170
                                });

                                if (file == null)
                                    return;
                                else
                                {
                                    while (fileSelectedPath == null)
                                        fileSelectedPath = file.Path;

                                    imgToWork.Source = ImageSource.FromStream(() => file.GetStreamWithImageRotatedForExternalStorage());
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
                        MaxWidthHeight = 170
                    });

                    if (file == null)
                        return;

                    imgToWork.Source = ImageSource.FromStream(() =>
                    {

                        while (fileSelectedPath == null)
                            fileSelectedPath = file.Path;

                        var stream = file.GetStreamWithImageRotatedForExternalStorage();
                        GrabarImageApi(stream);
                        return stream;
                    });
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        async private void GrabarImageApi(Stream st)
        {
            try
            {
                Acr.UserDialogs.UserDialogs.Instance.ShowLoading();
                var buffer = new byte[st.Length];
                st.Seek(0, SeekOrigin.Begin);
                st.Read(buffer, 0, buffer.Length);
                var base64 = Convert.ToBase64String(buffer);
                var result = await herramientas.SetPost<EMenu>("FastFood/GrabarImagen", new EMenu() { idmenu_fast_food = id, foto = base64 });

                if (result.result == "OK")
                {
                    App.url_foto_menu = result.foto;
                }
                else
                {
                    toastConfig.MostrarNotificacion($"No se pudo actualizar la foto del producto. Intente mas tarde.", ToastPosition.Top, 3, "#c82333");
                }
            }
            catch (Exception)
            {
                toastConfig.MostrarNotificacion($"No se pudo actualizar la foto del producto. Revise la conexión a internet.", ToastPosition.Top, 3, "#c82333");
            }
            finally
            {
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
            }

        }


        private async void TakePhotoAsync(Page senderPage, Image imgToWork)
        {

            try
            {
                fileSelectedPath = null;

                if (Device.RuntimePlatform == Device.Android)
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

                    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                    {
                        // MsgNormal(senderPage, "No hay cámara disponible", "OK", "Cámara");
                        return;
                    }

                    //Check for Camera Permisions
                    await CrossPermissions.Current.RequestPermissionsAsync(Plugin.Permissions.Abstractions.Permission.Camera);

                    var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    {
                        PhotoSize = PhotoSize.MaxWidthHeight,
                        MaxWidthHeight = 170
                    });

                    if (file == null)
                        return;
                    else
                    {
                        while (fileSelectedPath == null)
                            fileSelectedPath = file.Path;

                        imgToWork.Source = ImageSource.FromStream(() => file.GetStreamWithImageRotatedForExternalStorage());
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

                    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                    {
                        // MsgNormal("No Camera", ":( No camera available.", "OK");
                        return;
                    }

                    var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    {
                        PhotoSize = PhotoSize.MaxWidthHeight,
                        MaxWidthHeight = 170
                    });

                    if (file == null)
                        return;

                    var image = imgToWork.Source = ImageSource.FromStream(() =>
                    {

                        while (fileSelectedPath == null)
                            fileSelectedPath = file.Path;

                        var stream = file.GetStreamWithImageRotatedForExternalStorage();
                        GrabarImageApi(stream);
                        return stream;
                    });


                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }

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

        [Obsolete]
        async void BtnAgregarFoto_Clicked(System.Object sender, System.EventArgs e)
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
                                mediaFile = file;


                                if (file == null)
                                    return;
                                else
                                {
                                    while (fileSelectedPath == null)
                                        fileSelectedPath = file.Path;

                                    imgProducto.Source = ImageSource.FromStream(() => file.GetStreamWithImageRotatedForExternalStorage());
                                    if (mediaFile != null)
                                    {
                                        BtnAgregarAlMenu.IsVisible = true;
                                    }
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

                    mediaFile = file;

                    if (file == null)
                        return;

                    imgProducto.Source = ImageSource.FromStream(() =>
                    {

                        while (fileSelectedPath == null)
                            fileSelectedPath = file.Path;

                        var stream = file.GetStream();
                        return stream;
                    });

                    if (mediaFile != null)
                    {
                        BtnAgregarAlMenu.IsVisible = true;
                    }


                }
            }
            catch (Exception)
            {
                return;
            }


            //Metodo para tomar fotos
            //await CrossMedia.Current.Initialize();
            //if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            //{
            //    await DisplayAlert("No Camera", ":( No camera available.", "0K");
            //    return;
            //}


            //var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            //{
            //    Directory = "Sample",
            //    Name = "test.jpg",
            //    SaveToAlbum = true,
            //    CompressionQuality = 75,

            //    CustomPhotoSize = 50,

            //    PhotoSize = Plugin.Media.Abstractions.PhotoSize.MaxWidthHeight,
            //    MaxWidthHeight = 2000,
            //    DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Front
            //});

            //if (file == null)
            //    return;
            //await DisplayAlert("File Location", file.Path, "OK");

            //imgProducto.Source = ImageSource.FromStream(() =>
            //{
            //    var stream = file.GetStream();
            //    return stream;
            //});


            //try
            //{
            //    SeleccionarOTomarFoto(this, "Seleccionar Foto de Perfil", "Cancelar", imgProducto);

            //}
            //catch (Exception ex)
            //{

            //}
        }


        async void BtnVermenu_Clicked(System.Object sender, System.EventArgs e)
        {
            modalMenu = new ModalMenu();
            await PopupNavigation.PushAsync(modalMenu);
        }

        private void PickerDisponible_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker = PickerDisponible.SelectedItem.ToString();
        }

        private async void BtnEnviarNotificacion_Clicked(object sender, EventArgs e)
        {
            FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
            var datos = await metodos.EnviarNotificacon(TxtNotificaciones.Text, "S");

            if (datos.Respuesta == "OK")
            {
                toastConfig.MostrarNotificacion($"Notificación enviada a tus clientes", ToastPosition.Top, 3, "#51C560");
            }
            else
            {
                toastConfig.MostrarNotificacion($"Ocurrio un error, intenta nuevamente o comunicate con el administrador", ToastPosition.Top, 3, "#51C560");
            }
        }

        async void lsv_notificaciones_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
            var result = await DisplayAlert("Aviso", "¿Desea ocultar esta notificación?", "SI", "NO");
            if (result)
            {
                var obj = (ENotificaciones)e.SelectedItem;
                var ide = obj.disponibilidad;

                if (ide == "S")
                {
                    var datos = await metodos.ActualizarNotificacion(obj.idnotificaciones_empresa, "N");
                    toastConfig.MostrarNotificacion($"Esta notificación ya no será visible para tus clientes", ToastPosition.Top, 3, "#51C560");
                }
                else
                {
                    var datos = await metodos.ActualizarNotificacion(obj.idnotificaciones_empresa, "S");
                    toastConfig.MostrarNotificacion($"Esta notificación ahora será visible para tus clientes", ToastPosition.Top, 3, "#51C560");

                }
            }
            _ = LlenarNotificaciones();


        }

        private async void lsv_pedidosEmpresa_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (EPedidos)e.SelectedItem;
            App.idpedidos_fast_food = obj.idpedidos_fast_food;

            modalCambiarProgreso = new ModalCambiarProgreso();
            await PopupNavigation.PushAsync(modalCambiarProgreso);

        }
    }
}