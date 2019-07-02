//Coin Analysis Report 

        DataTable data = new DataTable();

        public string Note;

        string[] values = { "5000", "2000", "1000", "500", "100", "50", "20", "10", "5", "2", "1", "0.50", "0.25", "0.10", "0.05" };
        string[] valuess = { "5000", "1000", "500", "100", "50", "20", "10", "5", "2", "1", "0.50", "0.25", "0.10", "0.05" };
        private double amount;

        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public DataTable getCashAmount(int pYear, int pMonth, string pRoute, int ChkNotes)
        {
            data.Clear();
            createTable(ChkNotes);

            string q = "SELECT SupplierCode,RouteCode,PaymentDue FROM dbo.MonthlyPaymentSummary WHERE Year = '" + pYear + "' AND Month ='" + pMonth + "' AND RouteCode='" + pRoute + "' AND PaymentMode = 'Cash' AND PaymentDue > 0";
            DataTable dt = SQLHelper.FillDataSet(q, CommandType.Text).Tables[0];



            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Amount = String.IsNullOrEmpty(dt.Rows[i]["PaymentDue"].ToString()) ? 0 : Convert.ToDouble(dt.Rows[i]["PaymentDue"].ToString());

                if (Amount != 0)
                {
                    cashamt(ChkNotes);
                }
            }
            return data;
        }

        public DataTable getCashAmountAll(int pYear, int pMonth ,int ChkNotes)
        {
            data.Clear();
            createTable(ChkNotes);
           
            string q = "SELECT SupplierCode,RouteCode,PaymentDue FROM dbo.MonthlyPaymentSummary WHERE Year = '" + pYear + "' AND Month ='" + pMonth + "' AND PaymentMode = 'Cash' AND PaymentDue > 0";
            DataTable dt = SQLHelper.FillDataSet(q, CommandType.Text).Tables[0];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Amount = String.IsNullOrEmpty(dt.Rows[i]["PaymentDue"].ToString()) ? 0 : Convert.ToDouble(dt.Rows[i]["PaymentDue"].ToString());

                if (Amount != 0)
                {
                    cashamt(ChkNotes);
                }
            }
            return data;
        }

        private void findCoins(double value, int index)
        {

            int count = 0;

            count = (int)(Amount / value);
            Amount = Amount - (count * value);
            int preCount = string.IsNullOrEmpty(data.Rows[index]["count"].ToString()) ? 0 : Convert.ToInt32(data.Rows[index]["count"].ToString());
            data.Rows[index]["count"] = preCount + count;
        }

        public void cashamt(int ChkNotes)
        {
            if (ChkNotes == 1)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    findCoins(Convert.ToDouble(values[i]), i);
                }
            }
            else
            {
                for (int i = 0; i < valuess.Length; i++)
                {
                    findCoins(Convert.ToDouble(valuess[i]), i);
                }
            }
        }
                

        public void createTable(int ChkNotes)
        {
            if (ChkNotes == 1)
            {
                DataRow row;
                if (!data.Columns.Contains("value") || !data.Columns.Contains("count"))
                {
                    data.Columns.Add("value", typeof(string));
                    data.Columns.Add("count", typeof(string));
                }

                for (int i = 0; i < values.Length; i++)
                {
                    row = data.NewRow();
                    data.Rows.Add(row);
                    data.Rows[i]["value"] = values[i];
                }
            }
            else 
            {
                DataRow row;
                if (!data.Columns.Contains("value") || !data.Columns.Contains("count"))
                {
                    data.Columns.Add("value", typeof(string));
                    data.Columns.Add("count", typeof(string));
                }

                for (int i = 0; i < valuess.Length; i++)
                {
                    row = data.NewRow();
                    data.Rows.Add(row);
                    data.Rows[i]["value"] = valuess[i];
                }
            }
        }