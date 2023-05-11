$(function () {

   

    $(document).on("click", ".slider-status", function () {

        let sliderId = $(this).parent().attr("data-id")  
        let changeElem = $(this);  // this icine vermek olmur onu bir valeybula menimsedirik ve onu cagirikiqki her cliki gotursun
        let data = { id: sliderId };

        // id sini data adli variableye beraber edir bu wekilde gonderirik ajaxa

        $.ajax({
            url: `slider/setstatus`,     //setstatus Action request atiriq
            type: "post",
            data: data,                 //data ajaxsin ozunde olur gondereceyimiz data sayilir( yeni yuxaridaki variable deyil) ona beraber edirik data variableni gonderirik backende
            success: function (res) {

                if (res){
                    $(changeElem).removeClass("active-status");
                    $(changeElem).addClass("deActive-status");
                } else {
                    $(changeElem).addClass("active-status");
                    $(changeElem).removeClass("deActive-status");
                }
                

            }
        })

    })

    //delet category 


    $(document).on("click", "#category-delete-form", function (e) {

        e.preventDefault();

        let categoryId = $(this).attr("data-id")   //hemin categry delete butonuna aid olan productun Id-sini goturuk

        let deletedElem = $(this);  // this icine vermek olmur onu bir valeybula menimsedirik ve onu cagirikiqki her cliki gotursun
        let data = { id: categoryId };
        //productumuzun id sini data adli variableye beraber edir bu wekilde gonderirik ajaxa


        $.ajax({
            url: `category/softdelete`,     //category/softdelte Action request atiriq
            type: "Post",
            data: data,                 //data ajaxsin ozunde olur gondereceyimiz data sayilir( yeni yuxaridaki variable deyil) ona beraber edirik data variableni gonderirik backende
            success: function (res) {

                $(deletedElem).parent().parent().remove();

            }
        })

    })




    $(document).on("click", "#slider-info-delete-form", function (e) {

        e.preventDefault();

        let categoryId = $(this).attr("data-id")   //hemin categry delete butonuna aid olan productun Id-sini goturuk

       

        let deletedElem = $(this);  // this icine vermek olmur onu bir valeybula menimsedirik ve onu cagirikiqki her cliki gotursun
        let data = { id: categoryId };
        //productumuzun id sini data adli variableye beraber edir bu wekilde gonderirik ajaxa


        $.ajax({
            url: `SliderInfo/delete`,     //category/softdelte Action request atiriq
            type: "Post",
            data: data,                 //data ajaxsin ozunde olur gondereceyimiz data sayilir( yeni yuxaridaki variable deyil) ona beraber edirik data variableni gonderirik backende
            success: function (res) {

                $(deletedElem).parent().parent().remove();

            }
        })

    })




    $(document).on("click", "#expert-delete-form", function (e) {

        e.preventDefault();

        let categoryId = $(this).attr("data-id")   //hemin categry delete butonuna aid olan productun Id-sini goturuk



        let deletedElem = $(this);  // this icine vermek olmur onu bir valeybula menimsedirik ve onu cagirikiqki her cliki gotursun
        let data = { id: categoryId };
        //productumuzun id sini data adli variableye beraber edir bu wekilde gonderirik ajaxa


        $.ajax({
            url: `expert/delete`,     //category/softdelte Action request atiriq
            type: "Post",
            data: data,                 //data ajaxsin ozunde olur gondereceyimiz data sayilir( yeni yuxaridaki variable deyil) ona beraber edirik data variableni gonderirik backende
            success: function (res) {

                $(deletedElem).parent().parent().remove();

            }
        })

    })






})




