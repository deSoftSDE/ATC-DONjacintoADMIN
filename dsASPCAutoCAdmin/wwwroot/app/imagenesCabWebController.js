appadmin.controller('CabeceraWeb', function ($scope, Llamada, $timeout) {

    $scope.showColumnLines = false;
    $scope.showRowLines = true;
    $scope.showBorders = true;
    $scope.rowAlternationEnabled = true;
    $scope.dataGridOptions = {
        bindingOptions: {
            showColumnLines: "showColumnLines",
            showRowLines: "showRowLines",
            showBorders: "showBorders",
            rowAlternationEnabled: "rowAlternationEnabled"
        },
        dataSource: [],
        editing: {
            allowAdding: false, // Enables insertion
            allowDeleting: false, // Enables removing
            editEnabled: false
        },
        selection: {
            mode: "single"
        },
        
        /*onSelectionChanged: function (e) {
            console.log(e);
            alert("abierto");
            e.component.collapseAll(-1);
            e.component.expandRow(e.currentSelectedRowKeys[0]);
        },*/
        columns: [
            {
                dataField: "url",
                caption: "Imagen",
                width: "30%",
                allowFiltering: false,
                allowSorting: false,
                allowEditing: false,
                alignment: "center",
                cellTemplate: "cellTemplate"
            }, {
                dataField: "titulo",
                width: "30%",
                caption: "Titulo"
            }, {
                caption: "",
                width: "20%",
                allowFiltering: false,
                allowSorting: false,
                allowEditing: false,
                alignment: "center",
                cellTemplate: "editTemplate"
            }, {
                caption: "",
                width: "20%",
                allowFiltering: false,
                allowSorting: false,
                allowEditing: false,
                alingment: "center",
                cellTemplate: "deleteTemplate"
            }
        ],
        onInitialized: function (e) {
            console.log(e);
            $scope.datagrid = e.component;
            Llamada.get("ImagenesCabWebLeer")
                .then(function (respuesta) {
                    var cabeceras = respuesta.data;
                    for (i = 0; i < cabeceras.length; i++) {
                        cabeceras[i].url = Llamada.getRuta(cabeceras[i].imagenSt);
                    }
                    $scope.datagrid.option("dataSource", cabeceras);
                })
        },
    };
    

    $scope.files = "";
    $scope.guardarCambios = function (cabecera) {
        if (NotNullNotUndefinedNotEmpty(cabecera.files)) {
            console.log(document.getElementById("filesup").files);
            var fd = new FormData();
            fd.append('file.jpg', document.getElementById("filesup").files[0]);
            console.log(fd);
            console.log(cabecera.files);
            //console.log($scope.files);
            cabecera.url = cabecera.files.data;
            //console.log(vidrio.files.data);
            Llamada.postFile(fd)
                .then(function (respuesta) {
                    cabecera.imagenSt = respuesta[0].contenido;
                    document.getElementById("filesup").value = null;
                    guardarCambios(cabecera);
                });
        } else {
            cabecera.newImagen = cabecera.imagen;
            guardarCambios(cabecera);
        }

        $scope.datagrid.collapseAll(-1);
    };
    $scope.modificarCabecera = function (vid) {
        $scope.popupVisible = true;
        $scope.currentcabecera = vid.data;
    };
    guardarCambios = function (vehiculo) {
        vehiculo.files = null;
        Llamada.post("ImagenesCabWeb_Procesar", vehiculo)
            .then(function (respuesta) {
                mensajeExito("Datos guardados con éxito");
                var cabeceras = respuesta.data;
                for (i = 0; i < cabeceras.length; i++) {
                    cabeceras[i].url = Llamada.getRuta(cabeceras[i].imagenSt);
                }
                $scope.datagrid.option("dataSource", cabeceras);

                $scope.popupVisible = false;
            });

    };
    $scope.popupVisible = false;
    $scope.popupOptions = {
        width: 660,
        height: "auto",
        showTitle: true,
        title: "Editar/crear Cabecera Web",
        dragEnabled: false,
        bindingOptions: {
            visible: 'popupVisible'
        },
        closeOnOutsideClick: true
    };
    $scope.eliminarRegistro = function (a) {
        result = DevExpress.ui.dialog.confirm("¿Seguro que deseas eliminar esta cabecera?");
        result.then(function (val) {
            if (val) {
                console.log("OK");
                a.data.tipoTransaccion = 2;
                Llamada.post("ImagenesCabWeb_Procesar", a.data)
                    .then(function (respuesta) {
                        var cabeceras = respuesta.data;
                        for (i = 0; i < cabeceras.length; i++) {
                            cabeceras[i].url = Llamada.getRuta(cabeceras[i].imagenSt);
                        }
                        $scope.datagrid.option("dataSource", cabeceras);
                    });

              
            }
        });
        
    };
    
    $scope.crearRegistro = function () {
        //alert("Holi");
        $scope.popupVisible = true;
        $scope.currentcabecera = {
            titulo: "",
            tipoTransaccion: 1,
            url: Llamada.getRuta("")
        };
    };
    $scope.guardarCambiosPopup = function () {
        $scope.guardarCambios($scope.currentcabecera);
    };
    $scope.cancelarCambios = function () {
        $scope.popupVisible = false;
    };
    $scope.eliminarVidrio = function (vidrio, $index) {
        result = DevExpress.ui.dialog.confirm("¿Seguro que deseas eliminar este vidrio?");
        result.then(function (val) {
            if (val) {
                console.log("OK");
                $scope.$apply(function () {
                    vidrio.modificador = 2;
                });


                console.log(vidrio);
            }
        });
    };
});