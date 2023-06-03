using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace HTML5
{
    public class CircularProgressBar : Panel
    {
        private int _minimum = 0;
        private int _maximum = 100;
        private int _value = 0;
        private Color _progressColor = Color.White;
        private Color _textColor = Color.White;

        public CircularProgressBar()
        {
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            this.BackColor = Color.Transparent;
            this.ForeColor = Color.Black;
            this.Padding = new Padding(10);
            this.Size = new Size(100, 100);
        }

        [Category("Appearance")]
        public Color ProgressColor
        {
            get { return _progressColor; }
            set { _progressColor = value; }
        }

        [Category("Appearance")]
        public Color TextColor
        {
            get { return _textColor; }
            set { _textColor = value; }
        }

        [Category("Behavior")]
        public int Minimum
        {
            get { return _minimum; }
            set
            {
                _minimum = value;
                if (_minimum > _value)
                {
                    _value = _minimum;
                }
                this.Invalidate();
            }
        }

        [Category("Behavior")]
        public int Maximum
        {
            get { return _maximum; }
            set
            {
                _maximum = value;
                if (_maximum < _value)
                {
                    _value = _maximum;
                }
                this.Invalidate();
            }
        }

        [Category("Behavior")]
        public int Value
        {
            get { return _value; }
            set
            {
                if (value < _minimum)
                {
                    _value = _minimum;
                }
                else if (value > _maximum)
                {
                    _value = _maximum;
                }
                else
                {
                    _value = value;
                }
                this.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(this.Padding.Left, this.Padding.Top, this.Width - this.Padding.Horizontal, this.Height - this.Padding.Vertical);

            float sweepAngle = 360f / (_maximum - _minimum) * (_value - _minimum);

            using (Pen pen = new Pen(Color.FromArgb(100, Color.White), 8))
            {
                graphics.DrawArc(pen, rect, 0, 360);
                graphics.DrawArc(new Pen(_progressColor, 8), rect, -90, sweepAngle);
            }

            using (Font font = new Font("Open Sans", 18, FontStyle.Bold))
            {
                string text = $"{_value}%";
                SizeF textSize = graphics.MeasureString(text, font);
                Point textLocation = new Point((int)(this.Width / 2 - textSize.Width / 2.2), (int)(this.Height / 2 - textSize.Height / 2.2));
                using (SolidBrush brush = new SolidBrush(_textColor))
                {
                    graphics.DrawString(text, font, brush, textLocation);
                }
            }
        }
    }
}
