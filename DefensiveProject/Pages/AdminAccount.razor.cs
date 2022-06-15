using DefensiveProject.Components;

namespace DefensiveProject.Pages
{
    public partial class AdminAccount
    {
        bool addition = false;
        public void OnClick()
        {
            addition = true;
            editing = false;
            delete = false;
        }

        bool editing = false;
        void OnClick1()
        {
            editing = true;
            addition = false;
            delete = false;
        }
        bool delete = false;
        void OnClick2()
        {
            delete = true;
            editing = false;
            addition = false;
        }

    }
}

