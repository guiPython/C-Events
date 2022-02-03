using Program;

namespace Events
{
    public class InsertEventArgs
    {
        public string Query { get; set; }

        public InsertEventArgs(string query)
        {
            Query = query;
        }
    }
    public class Insert
    {
        public event EventHandler<InsertEventArgs>? InsertPerformed;
        public event EventHandler? InsertCompleted;

        protected virtual void OnInsertPerformed(string query)
        {
            if (InsertPerformed != null)
                InsertPerformed(this, new InsertEventArgs(query));
        }

        protected virtual void OnInsertCompleted()
        {
            if (InsertCompleted != null)
                InsertCompleted(this, EventArgs.Empty);
        }

        public void Run(string[] queries)
        {
            foreach (string query in queries)
            {
                OnInsertPerformed(query);
            }
            OnInsertCompleted();
        }
    }
}