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
        ModalNotificaciones modalNotificaciones = new ModalNotificaciones();
        ToastConfigClass toastConfig = new ToastConfigClass();
        Herramientas herramientas = new Herramientas();

        int id = 0;
        string Picker = "";
        private bool busy;
        MediaFile mediaFile;
        MediaFile mediaFileEmpresa;

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
                    StackLayoutMoney.IsVisible = false;
                    StackLayoutNotificacionesEmpresa.IsVisible = false;


                    btnPedidosEmpresa.Source = "TimeAmarillo";
                    btnMenuEmpresa.Source = "hamburgerSodaWhite.png";
                    btnMiPerfil.Source = "userBlanco";
                    btnMoney.Source = "moneyBlanco";
                    btnNotitifacionesEmpresa.Source = "bellWhite";
                    _ = LlenarPedidos("PENDIENTE");
                    PickerProgreso.SelectedItem = "PENDIENTE";
                }),
                NumberOfTapsRequired = 1
            });

            PickPhoto.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    EscogerFoto();
                }),
                NumberOfTapsRequired = 1
            });

            gridInicioEmpresa.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    StackLayoutPaginaPrincipal.IsVisible = true;
                    StackLayoutMiPerfil.IsVisible = false;
                    StackLayoutMoney.IsVisible = false;
                    StackLayoutNotificacionesEmpresa.IsVisible = false;

                    StackLayoutPedidosEmpresa.IsVisible = false;
                    btnPedidosEmpresa.Source = "TimeBlanco";
                    btnMenuEmpresa.Source = "hamburgerSodaAmarillo.png";
                    btnMiPerfil.Source = "userBlanco";
                    btnMoney.Source = "moneyBlanco";
                    btnNotitifacionesEmpresa.Source = "bellWhite";


                }),
                NumberOfTapsRequired = 1
            });

            gridNotificacionesEmpresa.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    StackLayoutPaginaPrincipal.IsVisible = false;
                    StackLayoutMiPerfil.IsVisible = false;
                    StackLayoutMoney.IsVisible = false;
                    StackLayoutPedidosEmpresa.IsVisible = false;
                    StackLayoutNotificacionesEmpresa.IsVisible = true;


                    btnPedidosEmpresa.Source = "TimeBlanco";
                    btnMenuEmpresa.Source = "hamburgerSodaWhite.png";
                    btnMiPerfil.Source = "userBlanco";
                    btnMoney.Source = "moneyBlanco";
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
                    StackLayoutMoney.IsVisible = false;
                    StackLayoutNotificacionesEmpresa.IsVisible = false;

                    StackLayoutMiPerfil.IsVisible = true;
                    btnPedidosEmpresa.Source = "TimeBlanco";
                    btnMenuEmpresa.Source = "hamburgerSodaWhite.png";
                    btnMiPerfil.Source = "userAmarillo";
                    btnMoney.Source = "moneyBlanco";
                    btnNotitifacionesEmpresa.Source = "bellWhite";

                    LlenarEmpresa();

                }),
                NumberOfTapsRequired = 1
            });

            gridMoney.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    StackLayoutPaginaPrincipal.IsVisible = false;
                    StackLayoutPedidosEmpresa.IsVisible = false;
                    StackLayoutMiPerfil.IsVisible = false;
                    StackLayoutMoney.IsVisible = true;
                    StackLayoutNotificacionesEmpresa.IsVisible = false;

                    btnPedidosEmpresa.Source = "TimeBlanco";
                    btnMenuEmpresa.Source = "hamburgerSodaWhite.png";
                    btnMiPerfil.Source = "userBlanco";
                    btnMoney.Source = "moneyAmarillo";
                    btnNotitifacionesEmpresa.Source = "bellWhite";

                    LlenarMoney();
                }),
                NumberOfTapsRequired = 1
            });
        }

        public async void LlenarMoney()
        {
            try
            {
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                var datos = await metodos.ObtenerPedidos("TRABAJADA", 0);
                lsv_money.ItemsSource = datos;
                lblBalance.Text = string.Format("{0:N2}", datos.Sum(n => n.total_por_producto));
                lblcantidad.Text = datos.Count.ToString();
            }
            catch (Exception ex)
            {

            }
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

                                    EmpresaFoto.Source = ImageSource.FromStream(() => file.GetStreamWithImageRotatedForExternalStorage());
                                    if (mediaFileEmpresa != null)
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

                    mediaFileEmpresa = file;

                    if (file == null)
                        return;

                    EmpresaFoto.Source = ImageSource.FromStream(() =>
                    {

                        while (fileSelectedPath == null)
                            fileSelectedPath = file.Path;

                        var stream = file.GetStream();
                        return stream;
                    });

                    if (mediaFileEmpresa != null)
                    {
                        BtnAgregarAlMenu.IsVisible = true;
                    }


                }
            }
            catch (Exception)
            {
                return;
            }

        }

        private void LlenarMiPerfilEmpresa()
        {
            TxtNombreEmpresa.Text = App.NombreEmpresa;
            TxtDireccionEmpresa.Text = App.direccionEmpresa;
            TxtWhatsappEmpresa.Text = App.whatsappEmpresa;
            TxtCorreoEmpresa.Text = App.correoEmpresa;
            TxtClaveEMpresa.Text = App.claveEmpresa;
            TxtPrecioEnvio.Text = App.envio.ToString();
            TxtTelefonoEmpresa.Text = App.telefonoEmpresa;
            EmpresaFoto.Source = App.url_foto_empresa;
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

        public async Task LlenarEmpresa()
        {
            try
            {
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                var datos = await metodos.ObtenerEmpresa();
                App.envio = datos[0].envio;
                TxtPrecioEnvio.Text = datos[0].envio.ToString();
                TxtNombreEmpresa.Text = datos[0].nombre;
                TxtDireccionEmpresa.Text = datos[0].direccion;
                App.rncEmpresa = datos[0].rnc;
                TxtTelefonoEmpresa.Text = datos[0].telefono;
                TxtWhatsappEmpresa.Text = datos[0].whatsapp;
                TxtCorreoEmpresa.Text = datos[0].correo;
                App.latitudEmpresa = datos[0].latitud;
                App.longitudeEmpresa = datos[0].longitud;
                TxtClaveEMpresa.Text = datos[0].clave;
                App.instagramEmpresa = datos[0].instagram;
                App.facebookEmpresa = datos[0].facebook;
                App.encargado_empresa = datos[0].encargado_empresa;
                App.encargado_empresa = datos[0].encargado_empresa;
                App.logo_empresa = datos[0].logo_empresa;
                EmpresaFoto.Source = datos[0].logo_empresa;
                App.idempresa = datos[0].idempresa;

            }
            catch (Exception ex)
            {

            }
        }

        public async Task LlenarPedidos(string progresoorden)
        {
            try
            {
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                var datos = await metodos.ObtenerPedidos(progresoorden, 0);
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


        async private void GrabarImageApiEmpresa(Stream st)
        {
            try
            {
                Acr.UserDialogs.UserDialogs.Instance.ShowLoading();
                var buffer = new byte[st.Length];
                st.Seek(0, SeekOrigin.Begin);
                st.Read(buffer, 0, buffer.Length);
                var base64 = Convert.ToBase64String(buffer);
                var result = await herramientas.SetPost<EEmpresa>("FastFood/GrabarImagenEmpesa", new EEmpresa() { idempresa = App.idempresa, logo_empresa = base64 });

                //if (result.result == "OK")
                //{
                //    App.url_foto_empresa = result.logo_empresa;
                //}
                //else
                //{
                //    toastConfig.MostrarNotificacion($"No se pudo actualizar la foto del producto. Intente mas tarde.", ToastPosition.Top, 3, "#c82333");
                //}
            }
            catch (Exception ex)
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
            string TxtNotificacionesArreglado = TxtNotificaciones.Text.Replace("\n", "").Replace("\r", "");

            var datos = await metodos.EnviarNotificacon(TxtNotificacionesArreglado, "S");

            if (datos.Respuesta == "OK")
            {
                toastConfig.MostrarNotificacion($"Notificación enviada a tus clientes", ToastPosition.Top, 3, "#51C560");
                TxtNotificaciones.Text = "";

            }
            else
            {
                toastConfig.MostrarNotificacion($"Ocurrio un error, intenta nuevamente o comunicate con el administrador", ToastPosition.Top, 3, "#51C560");
            }
        }



        private async void lsv_pedidosEmpresa_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (EPedidos)e.SelectedItem;
            App.idpedidos_fast_food = obj.idpedidos_fast_food;
            App.latitudPedido = obj.latitud;
            App.longitudPedido = obj.longitud;
            modalCambiarProgreso.Disappearing += ModalCambiarProgreso_Disappearing;

            modalCambiarProgreso = new ModalCambiarProgreso();
            await PopupNavigation.PushAsync(modalCambiarProgreso);

        }

        private void ModalCambiarProgreso_Disappearing(object sender, EventArgs e)
        {
            _ = LlenarPedidos("PENDIENTE");
        }

        private void btnEditarPerfilEmpresa_Clicked(object sender, EventArgs e)
        {
            TxtNombreEmpresa.IsEnabled = true;
            TxtDireccionEmpresa.IsEnabled = true;
            TxtTelefonoEmpresa.IsEnabled = true;
            TxtCorreoEmpresa.IsEnabled = true;
            TxtPrecioEnvio.IsEnabled = true;
            TxtWhatsappEmpresa.IsEnabled = true;
            TxtClaveEMpresa.IsEnabled = true;
            btnGuardarCambiosEmpresa.IsVisible = true;
            btnEditarPerfilEmpresa.IsVisible = false;
            PickPhoto.IsVisible = true;
        }

        private void btnGuardarCambiosEmpresa_Clicked(object sender, EventArgs e)
        {
            TxtNombreEmpresa.IsEnabled = false;
            TxtDireccionEmpresa.IsEnabled = false;
            TxtTelefonoEmpresa.IsEnabled = false;
            TxtCorreoEmpresa.IsEnabled = false;
            TxtWhatsappEmpresa.IsEnabled = false;
            TxtPrecioEnvio.IsEnabled = false;
            TxtClaveEMpresa.IsEnabled = false;
            btnGuardarCambiosEmpresa.IsVisible = false;
            btnEditarPerfilEmpresa.IsVisible = true;

            if (mediaFileEmpresa != null)
            {
                GrabarImageApiEmpresa(mediaFileEmpresa.GetStreamWithImageRotatedForExternalStorage());
            }

            ActualizarEmpresa(TxtNombreEmpresa.Text, TxtDireccionEmpresa.Text, TxtTelefonoEmpresa.Text, TxtWhatsappEmpresa.Text, TxtCorreoEmpresa.Text, TxtPrecioEnvio.Text, TxtClaveEMpresa.Text, App.idempresa);

        }

        private void eyebuttonnegro_Clicked(object sender, EventArgs e)
        {
            eyebuttonnegro.IsVisible = false;
            eyebuttonazul.IsVisible = true;
            TxtClaveEMpresa.IsPassword = false;
        }

        private void eyebuttonazul_Clicked(object sender, EventArgs e)
        {
            eyebuttonnegro.IsVisible = true;
            eyebuttonazul.IsVisible = false;
            TxtClaveEMpresa.IsPassword = true;
        }

        public async void ActualizarEmpresa(string nombreEmpresa, string DireccionEmpresa, string TelefonoEmpresa, string WhatsappEmpresa, string CorreoEmpresa, string PrecioEnvio, string ClaveEMpresa, int idempresa)
        {
            try
            {
                FastFoodApp.Metodos.Metodos metodos = new FastFoodApp.Metodos.Metodos();
                var datos = await metodos.ActualizarEmpresa(nombreEmpresa, DireccionEmpresa, TelefonoEmpresa, WhatsappEmpresa, CorreoEmpresa, PrecioEnvio, ClaveEMpresa, idempresa);
                if (datos.Respuesta == "OK")
                {
                    toastConfig.MostrarNotificacion($"Datos actualizados con exito", ToastPosition.Top, 3, "#51C560");
                    PickPhoto.IsVisible = false;
                    LlenarEmpresa();

                }
            }
            catch (Exception ex)
            {
                toastConfig.MostrarNotificacion($"Ocurrio un error, intenta nuevamente o comunicate con el administrador", ToastPosition.Top, 3, "#51C560");
            }
        }

        private async void BtnCerrarSesionEmpresa_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Información", "¿Desea cerrar sesión?", "SI", "NO"))
            {
                //DatosConfiguracion.GrabarDatosSesion("", "", "S");
                LoginPage loginPage = new LoginPage();
                await Navigation.PushModalAsync(loginPage);
                //DatosConfiguracion.EliminarDatosSesion();


            }
        }

        private void PickerProgreso_SelectedIndexChanged(object sender, EventArgs e)
        {
            App.ProgresoOrden = PickerProgreso.SelectedItem.ToString();
            _ = LlenarPedidos(App.ProgresoOrden);
        }

        public async void BtnVerNotificaciones_Clicked(object sender, EventArgs e)
        {
            modalNotificaciones = new ModalNotificaciones();
            await PopupNavigation.PushAsync(modalNotificaciones);
        }
    }
}