using System.Collections.Generic;

namespace WebApp.Helpers
{
 
        public class Paginate<T>
        {
            public List<T> Datas { get; set; }
            public int CurrentPageCount { get; set; }
            public int TotalPageCount { get; set; }

            public Paginate(List<T> datas, int currentPageCnt, int totalPageCnt)
            {
                Datas = datas;
                CurrentPageCount = currentPageCnt;
                TotalPageCount = totalPageCnt;
            }

            public bool HasAnyPrevious
            {
                get
                {
                    return CurrentPageCount > 1;
                }
            }

            public bool HasAnyNext
            {
                get
                {
                    return CurrentPageCount < TotalPageCount;
                }
            }
        }
    }

