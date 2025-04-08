$(document).on("change", "#CategoriaId.get-subcategories", function (e) {
    var categoriaId = $(this).val();

    if (categoriaId !== "") {
        $.ajax({
            url: "/Data/GetTicketSubCategories/" + categoriaId,
            type: "GET",
            dataType: "json",
            beforeSend: function () {
                $("#SubCategoriaId").html('<option value="">Carregando...</option>');
            },
            success: function (data) {
                $("#SubCategoriaId").html('<option value="">-- Selecione uma Subcategoria --</option>');
                console.log("Subcategorias recebidas:", data);

                // Preenchendo o select com as subcategorias retornadas
                $.each(data, function (index, item) {
                    $("#SubCategoriaId").append($('<option></option>').val(item.id).html(item.name));
                });
            },
            error: function (xhr, status, error) {
                console.log("Erro ao buscar subcategorias: ", error);
            }
        });
    } else {
        $("#SubCategoriaId").html('<option value="">-- Selecione uma Subcategoria --</option>');
    }
});
