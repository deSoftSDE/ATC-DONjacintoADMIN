



appadmin.controller('ModeloCarroceria', function ($scope, Llamada, $timeout) {
    console.log("Holi");

    $scope.verMarca = function (a) {
        console.log($scope.cambios)
        if ($scope.cambios === true) {
            result = DevExpress.ui.dialog.confirm("¿Seguro que deseas dejar de modificar este modelo sin guardar los cambios?");
            result.then(function (val) {
                if (val) {
                    irAMarca(a);
                }
            });
        } else {
            irAMarca(a);
        }
    };
    Llamada.get("ModelosLeerPorID?idFamilia=" + document.getElementById("idFamilia").value)
        .then(function (respuesta) {
            console.log(respuesta);
            $scope.currentmodelo = respuesta.data;
            $scope.currentmodelo.url = Llamada.getRuta($scope.currentmodelo.imagen);
            if (!NotNullNotUndefinedNotEmpty($scope.currentmodelo.imagenes)) {
                $scope.currentmodelo.imagenes = [];
            }
            for (i = 0; i < $scope.currentmodelo.imagenes.length; i++) {
                $scope.currentmodelo.imagenes[i].url = Llamada.getRuta($scope.currentmodelo.imagenes[i].valor);
                
            }
            $scope.datagrid.option("dataSource", $scope.currentmodelo.carrocerias);
            $scope.datagridImagenes.option("dataSource", $scope.currentmodelo.imagenes);
            
        });
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
        noDataText:"No se ha asociado ninguna carrocería a este modelo",
        dataSource: [],
        keyExpr: "id",
        editing: {
            allowAdding: false, // Enables insertion
            allowDeleting: false, // Enables removing
            editEnabled: false
        },
        selection: {
            mode: "single"
        },
        /*masterDetail: {
            enabled: false,
            template: "detail"
        },*/
        onRowInserted: function (info, a) {
            console.log(info);
            console.log(a);
        },
        onInitNewRow: function (info, b) {
            console.log("init");
            console.log(info);
            info.data.descripcion = 'Nuevo';
            console.log("HOLIII");
            info.component.saveEditData();
        },
        /*onSelectionChanged: function (e) {
            e.component.collapseAll(-1);
            e.component.expandRow(e.currentSelectedRowKeys[0]);
        },*/
        onRowRemoving: function (e) {
            console.log(e);
            eliminarRegistro(e.data.idCarroceria);
        },
        columns: [
            {
                dataField: "descripcion",
                width: "70%",
                caption: "Descripción"
            }, {
                caption: "Eliminar",
                width:"30%",
                allowFiltering: false,
                alignment: "center",
                allowSorting: false,
                allowEditing: false,
                cellTemplate: "deleteTemplate"
            }
        ],
        summary: {
            totalItems: [{
                column: "OrderNumber",
                summaryType: "count"
            }, {
                column: "OrderDate",
                summaryType: "min",
                customizeText: function (data) {
                    return "First: " + Globalize.formatDate(data.value, { date: "medium" });
                }
            }, {
                column: "SaleAmount",
                summaryType: "sum",
                valueFormat: "currency"
            }]
        },
        onInitialized: function (e) {
            console.log(e);
            $scope.datagrid = e.component;
        }
    };

    $scope.dataGridOptionsImagenes = {
        dataSource: [],
        keyExpr: "IdImagenModelo",
        editing: {
            allowAdding: false, // Enables insertion
            allowDeleting: false, // Enables removing
            editEnabled: false
        },
        noDataText:"No hay imágenes de este modelo para la galería",
        selection: {
            mode: "single"
        },
        columns: [
            {
                dataField: "url",
                caption: "Imagen",
                width: "30%",
                allowFiltering: false,
                alignment: "center",
                allowSorting: false,
                allowEditing: false,
                cellTemplate: "cellTemplate"
            }, {
                caption: "",
                width: 30,
                allowFiltering: false,
                alignment: "center",
                allowSorting: false,
                allowEditing: false,
                cellTemplate: "deleteTemplate"
            }
        ],
        summary: {
            totalItems: [{
                column: "OrderNumber",
                summaryType: "count"
            }, {
                column: "OrderDate",
                summaryType: "min",
                customizeText: function (data) {
                    return "First: " + Globalize.formatDate(data.value, { date: "medium" });
                }
            }, {
                column: "SaleAmount",
                summaryType: "sum",
                valueFormat: "currency"
            }]
        },
        onInitialized: function (e) {
            console.log(e);
            $scope.datagridImagenes = e.component;
        }
    };


    $scope.files = "";
    $scope.guardarCambios = function (carroceria) {
        guardarCambios(carroceria);
        $scope.datagrid.collapseAll(-1);
    };
    $scope.guardarCambiosModelo = function () {
        $scope.cambios = false;
        if (NotNullNotUndefinedNotEmpty($scope.currentmodelo.files)) {
            console.log(document.getElementById("filesup").files);
            var fd = new FormData();
            fd.append('file.jpg', document.getElementById("filesup").files[0]);
            console.log(fd);
            console.log($scope.currentmodelo.files);
            //console.log($scope.files);
            $scope.currentmodelo.url = $scope.currentmodelo.files.data;
            //console.log(vidrio.files.data);
            Llamada.postFile(fd)
                .then(function (respuesta) {
                    $scope.currentmodelo.imagen = respuesta[0].contenido;
                    document.getElementById("filesup").value = null;
                    guardarCambiosModelo($scope.currentmodelo);
                });
        } else {
            $scope.currentmodelo.newImagen = $scope.currentmodelo.imagen;
            guardarCambiosModelo();
        }

        $scope.datagrid.collapseAll(-1);
    };
    guardarCambiosModelo = function () {
        recorrerImagenes(0);
    };
    recorrerImagenes = function (indice) {
        if (indice < $scope.currentmodelo.imagenes.length) {
            if (NotNullNotUndefinedNotEmpty($scope.currentmodelo.imagenes[indice].fdata)) {
                Llamada.postFile($scope.currentmodelo.imagenes[indice].fdata)
                    .then(function (respuesta) {
                        //alert("Subida");
                        $scope.currentmodelo.imagenes[indice].valor = respuesta[0].contenido;
                        indice++;
                        recorrerImagenes(indice);
                    });
            } else {
                indice++;
                recorrerImagenes(indice);
            }
        } else {
            guardarCambiosFinal();
        }
    };
    guardarCambiosFinal = function() {
        $scope.currentmodelo.files = null;
        Llamada.post("ModelosCrearModificar", $scope.currentmodelo)
            .then(function (respuesta) {
                mensajeExito("Datos guardados con éxito");
                console.log(respuesta);
            });
    }
    $scope.modificarCarroceria = function (vid) {
        console.log(vid);
        Llamada.get("CarroceriasLeerPorID?IDCarroceria=" + vid.data.idCarroceria)
            .then(function (respuesta) {
                $scope.popupVisible = true;
                $scope.currentcarroceria = respuesta.data;
            });
    };
    guardarCambios = function (carroceria) {
        carroceria.files = null;
        console.log(carroceria);
        Llamada.post("CarroceriasCrearModificar", carroceria)
            .then(function (respuesta) {
                mensajeExito("Datos guardados con éxito");
                if (ZeroSiNull(carroceria.idCarroceria) < 1) {
                    var obj = {
                        tipo: "Carrocerias",
                        cadena: "",
                        accionPagina: "N",
                        lastValor: carroceria.descripcion,
                        lastIndice: respuesta.data.identidad
                    };
                    carroceria.idCarroceria = respuesta.identidad;
                    LeerRegistros(obj, carroceria);
                }
                console.log(respuesta);

                $scope.popupVisible = false;
            });
        $scope.popupVisible = false;

    };
    $scope.popupVisible = false;
    $scope.popupOptions = {
        width: "80%",
        height: "80%",
        showTitle: true,
        title: "Nueva Carrocería",
        fullScreen: false,
        dragEnabled: false,
        bindingOptions: {
            visible: 'popupVisible'
        },
        closeOnOutsideClick: true
    };
    $scope.cambioInput = function () {
        alert("Holi");
    };
    $scope.eliminarImagen = function (a) {
        result = DevExpress.ui.dialog.confirm("¿Seguro que deseas eliminar esta imagen?");
        result.then(function (val) {
            if (val) {
                var b = $scope.currentmodelo.imagenes.splice(a.rowIndex, 1);
                $scope.datagridImagenes.option("dataSource", $scope.currentmodelo.imagenes);
                if (!NotNullNotUndefinedNotEmpty($scope.currentmodelo.imagenesEliminadas)) {
                    $scope.currentmodelo.imagenesEliminadas = [];
                }
                $scope.currentmodelo.imagenesEliminadas.push(b[0]);
            }
        });
    }
    $scope.eliminarRegistro = function (id) {
        result = DevExpress.ui.dialog.confirm("¿Seguro que deseas eliminar esta carrocería de este modelo?");
        result.then(function (val) {
            if (val) {
                $scope.cambios = true;
                console.log(id.rowIndex);
                var b = $scope.currentmodelo.carrocerias.splice(id.rowIndex, 1);
                $scope.datagrid.option("dataSource", $scope.currentmodelo.carrocerias);
                if (!NotNullNotUndefinedNotEmpty($scope.currentmodelo.carroceriasEliminadas)) {
                    $scope.currentmodelo.carroceriasEliminadas = [];
                }
                $scope.currentmodelo.carroceriasEliminadas.push(b[0]);
                /*Llamada.get("CarroceriasEliminar?idCarroceria=" + id.data.idCarroceria)
                    .then(function (respuesta) {
                        console.log(respuesta);
                        
                            LeerRegistros($scope.lastConsulta);
                    });*/ console.log($scope.currentmodelo);
            }
        });
    };
    $scope.hola = function () {
        alert("Hola");
    };

    $scope.currentcarroceria = {
        descripcion: "Descripción"
    };
    $scope.crearRegistro = function () {
        $scope.popupVisible = true;
        $scope.currentcarroceria = {
            descripcion: "",
            url: Llamada.getRuta(""),
        };
    };
    $scope.anadirCarroceria = function (r) {
        if (!NotNullNotUndefinedNotEmpty($scope.currentmodelo.carrocerias)) {
            $scope.currentmodelo.carrocerias = [];
        }
        if (!$scope.estaEnListado(r.idCarroceria)) {
            $scope.currentmodelo.carrocerias.push(r);
            console.log($scope.currentmodelo);
            $scope.datagrid.option("dataSource", $scope.currentmodelo.carrocerias);
        } else {
            mensajeError("La carrocería seleccionada ya forma parte del modelo.");
        }
        
    };
    $scope.selected = 0;
    $scope.guardarCambiosPopup = function () {
        $scope.guardarCambios($scope.currentcarroceria);
    };
    $scope.cambiarPagina = function (sender, val) {
        cambiarBotonesPaginacion("");
        switch (val) {
            case "F":
                cambiarBotonesPaginacionIniciales("disabled");
                break;
            case "L":
                cambiarBotonesPaginacionFinales("disabled");
                break;
        }
        $scope.vm.cm.accionPagina = val;
        LeerRegistros($scope.vm.cm);
    };
    fnocultar = function () {
        $scope.mostrardesplegable = false;
    };
    $scope.focofuera = function (a, b) {
        $timeout(fnocultar, 1000);
    };
    $scope.entrafoco = function () {
        if (NotNullNotUndefinedNotEmpty($scope.resultadobusqueda)) {
            $scope.mostrardesplegable = true;
        }
    };
    fnocultar2 = function () {
        $scope.mostrardesplegable2 = false;
    };
    $scope.focofuera2 = function (a, b) {
        $timeout(fnocultar2, 1000);
    };
    $scope.entrafoco2 = function () {
        if (NotNullNotUndefinedNotEmpty($scope.resultadobusqueda2)) {
            $scope.mostrardesplegable = true;
        }
    };
    buscarArticulos = function () {
        $scope.loading = true;
        if ($scope.cadena.length > 0) {
            Llamada.get("TiposVidrioLeerPorCadena?cadena=" + $scope.cadena)
                .then(function (respuesta) {
                    $scope.resultadobusqueda = respuesta.data;
                    //$scope.NumReg = respuesta.data.numReg;
                    $scope.mostrardesplegable = true;
                    $scope.loading = false;
                    document.getElementById("desplegable").style.display = "block";
                });
        } else {
            $scope.resultadobusqueda = [];
            $scope.NumReg = 0;
            $scope.loading = false;
        }
    };
    buscarArticulos2 = function () {
        $scope.loading2 = true;
        if ($scope.cadena2.length > 0) {
            Llamada.get("CarroceriasLeerPorCadena?cadena=" + $scope.cadena2)
                .then(function (respuesta) {
                    $scope.resultadobusqueda2 = respuesta.data;
                    //$scope.NumReg = respuesta.data.numReg;
                    $scope.mostrardesplegable2 = true;
                    $scope.loading2 = false;
                    document.getElementById("desplegable2").style.display = "block";
                });
        } else {
            $scope.resultadobusqueda2 = [];
            $scope.NumReg = 0;
            $scope.loading2 = false;
        }
    };
    var inputChangedPromise;
    $scope.cambiobusqueda = function () {
        if (inputChangedPromise) {
            $timeout.cancel(inputChangedPromise);
        }
        inputChangedPromise = $timeout(buscarArticulos, 1000);
    };
    var inputChangedPromise2;
    $scope.cambiobusqueda2 = function () {
        if (inputChangedPromise2) {
            $timeout.cancel(inputChangedPromise2);
        }
        inputChangedPromise2 = $timeout(buscarArticulos2, 1000);
    };
    $scope.anadirVidrio = function (res) {
        console.log(res);
        $scope.mostrardesplegable = false;
        if (!NotNullNotUndefinedNotEmpty($scope.currentcarroceria.vidrios)) {
            $scope.currentcarroceria.vidrios = [];
        }
        var vid = {
            IDVidrio: 0,
            Imagen: res.imagen,
            descripcion: res.descripcion,
            IdTipoVidrio: res.idTipoVidrio,
            modificador: 1
        };
        $scope.currentcarroceria.vidrios.push(vid);
    };
    $scope.noEliminado = function (vidrio) {
        if (NotNullNotUndefinedNotEmpty(vidrio)) {
            if (NotNullNotUndefinedNotEmpty(vidrio.modificador)) {
                if (vidrio.modificador > 1) {
                    return false;
                } else {
                    return true;
                }
            } else {
                return true;
            }
        } else {
            return false;
        }
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
    $scope.cancelarCambios = function () {
        $scope.popupVisible = false;
    };
    $scope.cancelarCambiosImagen = function () {
        $scope.popupImagenVisible = false;
    };
    $scope.estaEnListado = function (val) {
        if (!NotNullNotUndefinedNotEmpty($scope.currentmodelo.carrocerias)) {
            $scope.currentmodelo.carrocerias = [];
        }
        res = false;
        for (i = 0; i < $scope.currentmodelo.carrocerias.length; i++) {
            if ($scope.currentmodelo.carrocerias[i].idCarroceria == val) {
                res = true;
            }
        }
        return res;
    }
    $scope.popupImagenVisible = false;
    $scope.popupImagen = {
        width: "400px",
        height: "300px",
        showTitle: true,
        title: "Imágenes Galería",
        fullScreen: false,
        dragEnabled: false,
        bindingOptions: {
            visible: 'popupImagenVisible'
        },
        closeOnOutsideClick: true
    };
    $scope.nuevaImagen = function () {
        $scope.popupImagenVisible = true;
    }
        

    $scope.guardarCambiosImagen = function () {
        console.log($scope.newImagen);
        var b = document.getElementById("imagenpopup").files;
        if (!NotNullNotUndefinedNotEmpty($scope.currentmodelo.imagenes)) {
            $scope.currentmodelo.imagenes = [];
        }
        for (n = 0; n < b.length; n++) {
            var fd = new FormData();
            fd.append('file.jpg', b[n]);
            $scope.popupImagenVisible = false;
            newImagen = {
                url: obtenerImagenInput(b[n]),
                fdata: fd,
            }
            $scope.currentmodelo.imagenes.push(newImagen);
        }
        document.getElementById("imagenpopup").value = null
        $scope.datagridImagenes.option("dataSource", $scope.currentmodelo.imagenes);
    }
});