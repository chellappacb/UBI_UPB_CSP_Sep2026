<%@ Page Language="C#" AutoEventWireup="true" CodeFile="image.aspx.cs" Inherits="image" %>
<%@ Import Namespace="System.Drawing" %>
<%@ Import Namespace="System.Drawing.Text" %>
<%@ Import Namespace="System.Drawing.Imaging" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <title>Untitled Page</title>
</head>
<body>
<script runat="server">
private Random rnd;

protected void Page_Load(object sender, EventArgs e)
{
    int x, y;
    string strKey = null;
    rnd = new Random();

    Response.ContentType = "image/jpeg";
    Response.Clear();
    Response.BufferOutput = true;

    strKey = GenerateString(5);
    Session["key"] = strKey.GetHashCode();

    Font font = new Font("Arial", (float)rnd.Next(18, 24), FontStyle.Bold);
    Bitmap bitmap = new Bitmap(200, 50);
    Graphics gr = Graphics.FromImage(bitmap);
    Color black = Color.Black;
    Color line = Color.FromArgb(165, 68, 68);
    SolidBrush brush = new SolidBrush(line);

    gr.FillRectangle(Brushes.White, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
    gr.DrawString(strKey, font, brush, (float)rnd.Next(70), (float)rnd.Next(20));

    //gr.DrawCurve(new Pen(line, (float)rnd.Next(1, 3)), new Point[] { new Point(0, rnd.Next(50)), new Point(rnd.Next(200), rnd.Next(50)), new Point(rnd.Next(200), rnd.Next(50)), new Point(rnd.Next(200), rnd.Next(50)), new Point(200, rnd.Next(50)) });
    //gr.DrawLine(new Pen(line, (float)rnd.Next(1, 3)), new Point(0, rnd.Next(50)), new Point(200, rnd.Next(50)));
    //gr.DrawLine(new Pen(black, (float)rnd.Next(1, 3)), new Point(0, rnd.Next(50)), new Point(200, rnd.Next(50)));
    //gr.DrawLine(new Pen(line, (float)rnd.Next(1, 3)), new Point(0, rnd.Next(50)), new Point(200, rnd.Next(50)));

    for (x = 0; x < bitmap.Width; x++)
        for (y = 0; y < bitmap.Height; y++)
            if (rnd.Next(6) == 1)
                bitmap.SetPixel(x, y, Color.FromArgb(241, 222, 194));

    font.Dispose();
    gr.Dispose();
    bitmap.Save(Response.OutputStream, ImageFormat.Jpeg);
    bitmap.Dispose();
}

private string GenerateString(int length)
{
    string validatingText = String.Empty;
    Int32 seed = new Int32();
    rnd = new Random();

    for (int a = 0; a < length; a++)
    {
        do
        {
            seed = rnd.Next(0, 61);
        }
        while (seed == 0 || seed == 1 || seed == 18 || seed == 24 || seed == 44 || seed == 50);
        if (seed < 10)
            seed += 48;
        else if (seed < 36)
            seed += 55;
        else
            seed += 61;
        char chr = (char)seed;
        validatingText += chr.ToString();
    }
    return validatingText;
}
</script>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>

</html>
