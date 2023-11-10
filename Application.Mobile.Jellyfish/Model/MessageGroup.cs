using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace MobileApp.Model
{
    public class MessageGroup : ObservableCollection<Message>
    {
        public DateOnly Date { get; private set; }
        public int SortKey
        {
            get
            {
                int n = 0;

                if (int.TryParse(Date.ToString("yyyyMMdd"), out n))
                {
                    return n;
                }
                return n;
            }
        }
        public override string ToString()
        {
            return Date.ToString("dd.MM.yyyy");
        }
        public MessageGroup(DateOnly date)
        {
            Date = date;

        }

    }
}
