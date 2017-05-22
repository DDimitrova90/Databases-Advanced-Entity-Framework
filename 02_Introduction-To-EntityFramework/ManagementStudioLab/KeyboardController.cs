namespace ManagementStudioLab
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class KeyboardController
    {
        public static bool PageController(ConsoleKeyInfo key, Paginator paginator)
        {
            switch (key.Key.ToString())
            {
                /*case "Enter":
                    Project currentProject = projects.Skip((pageSize * page) + pointer - 1).First();
                    ShowDetails(currentProject);
                    break;*/
                case "UpArrow":
                    if (paginator.CursorPos > 1)
                    {
                        paginator.CursorPos--;
                    }
                    else if (paginator.CurrentPage > 0)
                    {
                        paginator.CurrentPage--;
                        paginator.CursorPos = paginator.PageSize;
                    }
                    break;
                case "DownArrow":
                    if (paginator.CursorPos < paginator.PageSize)
                    {
                        int finalPage = paginator.Data.Count % paginator.PageSize;

                        if ((paginator.CurrentPage == paginator.MaxPages - 1) && 
                            (paginator.CursorPos >= finalPage))
                        {
                            break;
                        }

                        paginator.CursorPos++;
                    }
                    else if (paginator.CurrentPage + 1 <= paginator.MaxPages)
                    {
                        paginator.CurrentPage++;
                        paginator.CursorPos = 1;
                    }
                    break;
                case "Escape":
                    return false;
            }

            return true;
        }
    }
}
