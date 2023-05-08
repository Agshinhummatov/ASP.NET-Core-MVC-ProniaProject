namespace Pronia_BackEnd_Project.Helpers
{

    public class Paginate<T>
    {
        public IEnumerable<T> Datas { get; set; }  // datlarini verir 
        public int CurrentPage { get; set; } // olduqu seyfeni verir yeni page 
        public int TotalPage { get; set; } // butun seyfelerin countunu verir yeni total nece dene page var onu


        public Paginate(IEnumerable<T> datas, int currentPage, int totalPage)
        {
            Datas = datas;
            CurrentPage = currentPage;
            TotalPage = totalPage;
        }


        public bool HasPrevious  // meyyen sert daxilinde datani geri qaytarir yeni encapsilationdur 
        {

            get
            {
                return CurrentPage > 1;   // bu neynir boyukdu 1 den boyuk olanda True olcaq   1 den kicik olanda False olcaq  gedib orda 
            }
        }

        public bool HasNext
        {

            get
            {
                return CurrentPage < TotalPage;  // bu ise doruki tutaqki 5 ci pagdeyem 6 ci pagde de en sonucndu bu zaman next olsun gelib sertim berber olursa yeni current page== totalPage netx olmasin
            }
        }

    }

}
