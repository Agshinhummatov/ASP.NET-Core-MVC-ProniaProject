

$(document).on("click", ".categoriesName", function (e) {
    e.preventDefault();
    e.stopPropagation();
   
    let categoryId = $(this).parent().attr("data-id")
    console.log(categoryId)

    let data = { id: categoryId }


    $.ajax({
        url: "Shop/GetCategoryProducts",
        type: "Get",
        data: data,
        success: function (res) {

          

            $(".product-list").html(res)


        }
    })
})



$(document).on("click", ".allCategoriesName", function (e) {
    e.preventDefault();
    e.stopPropagation();

   


    $.ajax({
        url: "Shop/GetAllCategoriesProducts",
        type: "Get",
        
        success: function (res) {



            $(".product-list").html(res)


        }
    })
})





