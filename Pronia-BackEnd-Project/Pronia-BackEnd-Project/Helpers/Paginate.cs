namespace Pronia_BackEnd_Project.Helpers
{

    public class Paginate<T>
    {
        public IEnumerable<T> Datas { get; set; }  
        public int CurrentPage { get; set; }  
        public int TotalPage { get; set; } 


        public Paginate(IEnumerable<T> datas, int currentPage, int totalPage)
        {
            Datas = datas;
            CurrentPage = currentPage;
            TotalPage = totalPage;
        }


        public bool HasPrevious 
        {

            get
            {
                return CurrentPage > 1;   
            }
        }

        public bool HasNext
        {

            get
            {
                return CurrentPage < TotalPage;  
            }
        }

    }

}
