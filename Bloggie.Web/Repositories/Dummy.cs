namespace Bloggie.Web.Repositories
{

    public interface IDummy
    {
        void PrintName(string name);
        void PrintDescription(string description);
    }

    public class Dummy :IDummy
    {
        public void PrintName(string name)
        {
            //print
        }
        public void PrintDescription(string desc)
        {
            //print desc
        }
    }


    public class ProcessP
    {
        private IDummy _dummy;
        public ProcessP(IDummy dum)
        {
            _dummy = dum;

        }

        public void PP()
        {
            _dummy.PrintName("Bala");
        }
    }
}
