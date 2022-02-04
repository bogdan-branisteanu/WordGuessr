namespace src.Components{
    public class Button{
        public string Color { get; set; }

        public string sId { get; set; }

        public string Text { get; set; }

        public string Icon { get; set; }

        public string width { get; set; }

        public string height { get; set; }

        public string State { get; set; }

        public Button(string sId, string width, string height, string Color, string Text, string Icon){
            this.sId    = sId;
            this.width  = width;
            this.height = height;
            this.Color  = Color;
            this.Text   = Text;
            if(Icon != ""){
                this.Icon  = Icon;
            }
        }

        public Button(string sId, string width, string height, string Color, string Text){
            this.sId    = sId;
            this.width  = width;
            this.height = height;
            this.Color  = Color;
            this.Text   = Text;
        }


        
    }
}