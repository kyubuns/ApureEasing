using System;
using UnityEngine;

namespace ApureEasing
{
    public enum Easing
    {
        Linear,
        InBack,
        OutBack,
        InOutBack,
        InBounce,
        OutBounce,
        InOutBounce,
        InCirc,
        OutCirc,
        InOutCirc,
        InCubic,
        OutCubic,
        InOutCubic,
        InElastic,
        OutElastic,
        InOutElastic,
        InExpo,
        OutExpo,
        InOutExpo,
        InQuad,
        OutQuad,
        InOutQuad,
        InQuart,
        OutQuart,
        InOutQuart,
        InQuint,
        OutQuint,
        InOutQuint,
        InSine,
        OutSine,
        InOutSine,
    }

    public static class EasingConvert
    {
        public static float Get(Easing t, float v)
        {
            return t switch
            {
                Easing.Linear => Linear.Calc(v),
                Easing.InBack => InBack.Calc(v),
                Easing.OutBack => OutBack.Calc(v),
                Easing.InOutBack => InOutBack.Calc(v),
                Easing.InBounce => InBounce.Calc(v),
                Easing.OutBounce => OutBounce.Calc(v),
                Easing.InOutBounce => InOutBounce.Calc(v),
                Easing.InCirc => InCirc.Calc(v),
                Easing.OutCirc => OutCirc.Calc(v),
                Easing.InOutCirc => InOutCirc.Calc(v),
                Easing.InCubic => InCubic.Calc(v),
                Easing.OutCubic => OutCubic.Calc(v),
                Easing.InOutCubic => InOutCubic.Calc(v),
                Easing.InElastic => InElastic.Calc(v),
                Easing.OutElastic => OutElastic.Calc(v),
                Easing.InOutElastic => InOutElastic.Calc(v),
                Easing.InExpo => InExpo.Calc(v),
                Easing.OutExpo => OutExpo.Calc(v),
                Easing.InOutExpo => InOutExpo.Calc(v),
                Easing.InQuad => InQuad.Calc(v),
                Easing.OutQuad => OutQuad.Calc(v),
                Easing.InOutQuad => InOutQuad.Calc(v),
                Easing.InQuart => InQuart.Calc(v),
                Easing.OutQuart => OutQuart.Calc(v),
                Easing.InOutQuart => InOutQuart.Calc(v),
                Easing.InQuint => InQuint.Calc(v),
                Easing.OutQuint => OutQuint.Calc(v),
                Easing.InOutQuint => InOutQuint.Calc(v),
                Easing.InSine => InSine.Calc(v),
                Easing.OutSine => OutSine.Calc(v),
                Easing.InOutSine => InOutSine.Calc(v),
                _ => throw new ArgumentOutOfRangeException(nameof(t), t, null)
            };
        }
    }

    public class Linear
    {
        public static float Calc(float v)
        {
            return v;
        }
    }

    public class InBack
    {
        public static float Calc(float v)
        {
            return v * v * v - v * Mathf.Sin(v * Mathf.PI);
        }
    }

    public class OutBack
    {
        public static float Calc(float v)
        {
            var f = (1f - v);
            return 1f - (f * f * f - f * Mathf.Sin(f * Mathf.PI));
        }
    }

    public class InOutBack
    {
        public static float Calc(float v)
        {
            if (v < 0.5f)
            {
                var f = 2f * v;
                return 0.5f * (f * f * f - f * Mathf.Sin(f * Mathf.PI));
            }
            else
            {
                var f = (1 - (2 * v - 1));
                return 0.5f * (1f - (f * f * f - f * Mathf.Sin(f * Mathf.PI))) + 0.5f;
            }
        }
    }

    public class InBounce
    {
        public static float Calc(float v)
        {
            return Bounce(v);
        }

        public static float Bounce(float v)
        {
            return 1 - OutBounce.Bounce(1 - v);
        }
    }

    public class OutBounce
    {
        public static float Calc(float v)
        {
            return Bounce(v);
        }

        public static float Bounce(float v)
        {
            if (v < 4f / 11.0f)
            {
                return (121f * v * v) / 16.0f;
            }
            else if (v < 8f / 11.0f)
            {
                return (363f / 40.0f * v * v) - (99f / 10.0f * v) + 17f / 5.0f;
            }
            else if (v < 9f / 10.0f)
            {
                return (4356f / 361.0f * v * v) - (35442f / 1805.0f * v) + 16061f / 1805.0f;
            }
            else
            {
                return (54f / 5.0f * v * v) - (513f / 25.0f * v) + 268f / 25.0f;
            }
        }
    }

    public class InOutBounce
    {
        public static float Calc(float v)
        {
            if (v < 0.5f)
            {
                return 0.5f * InBounce.Bounce(v * 2f);
            }
            else
            {
                return 0.5f * OutBounce.Bounce(v * 2f - 1f) + 0.5f;
            }
        }
    }

    public class InCirc
    {
        public static float Calc(float v)
        {
            return 1f - Mathf.Sqrt(1f - (v * v));
        }
    }

    public class OutCirc
    {
        public static float Calc(float v)
        {
            return Mathf.Sqrt((2f - v) * v);
        }
    }

    public class InOutCirc
    {
        public static float Calc(float v)
        {
            if (v < 0.5f)
            {
                return 0.5f * (1 - Mathf.Sqrt(1f - 4f * (v * v)));
            }
            else
            {
                return 0.5f * (Mathf.Sqrt(-((2f * v) - 3f) * ((2f * v) - 1f)) + 1f);
            }
        }
    }

    public class InCubic
    {
        public static float Calc(float v)
        {
            return v * v * v;
        }
    }

    public class OutCubic
    {
        public static float Calc(float v)
        {
            var f = (v - 1f);
            return f * f * f + 1f;
        }
    }

    public class InOutCubic
    {
        public static float Calc(float v)
        {
            if (v < 0.5f)
            {
                return 4f * v * v * v;
            }
            else
            {
                var f = ((2f * v) - 2f);
                return 0.5f * f * f * f + 1f;
            }
        }
    }

    public class InElastic
    {
        public static float Calc(float v)
        {
            return Mathf.Sin(13 * (Mathf.PI / 2f) * v) * Mathf.Pow(2f, 10f * (v - 1f));
        }
    }

    public class OutElastic
    {
        public static float Calc(float v)
        {
            return Mathf.Sin(-13 * (Mathf.PI / 2f) * (v + 1)) * Mathf.Pow(2f, -10f * v) + 1f;
        }
    }

    public class InOutElastic
    {
        public static float Calc(float v)
        {
            if (v < 0.5f)
            {
                return 0.5f * Mathf.Sin(13f * (Mathf.PI / 2f) * (2f * v)) * Mathf.Pow(2f, 10f * ((2f * v) - 1f));
            }
            else
            {
                return 0.5f * (Mathf.Sin(-13f * (Mathf.PI / 2f) * ((2f * v - 1f) + 1f)) * Mathf.Pow(2f, -10f * (2f * v - 1f)) + 2f);
            }
        }
    }

    public class InExpo
    {
        public static float Calc(float v)
        {
            return Mathf.Approximately(0.0f, v) ? v : Mathf.Pow(2f, 10f * (v - 1f));
        }
    }

    public class OutExpo
    {
        public static float Calc(float v)
        {
            return Mathf.Approximately(1.0f, v) ? v : 1f - Mathf.Pow(2f, -10f * v);
        }
    }

    public class InOutExpo
    {
        public static float Calc(float v)
        {
            if (Mathf.Approximately(0.0f, v) || Mathf.Approximately(1.0f, v)) return v;

            if (v < 0.5f)
            {
                return 0.5f * Mathf.Pow(2f, (20f * v) - 10f);
            }
            else
            {
                return -0.5f * Mathf.Pow(2f, (-20f * v) + 10f) + 1f;
            }
        }
    }

    public class InQuad
    {
        public static float Calc(float v)
        {
            return v * v;
        }
    }

    public class OutQuad
    {
        public static float Calc(float v)
        {
            return -(v * (v - 2f));
        }
    }

    public class InOutQuad
    {
        public static float Calc(float v)
        {
            if (v < 0.5f)
            {
                return 2f * v * v;
            }
            else
            {
                return -2f * v * v + 4f * v - 1f;
            }
        }
    }

    public class InQuart
    {
        public static float Calc(float v)
        {
            return v * v * v * v;
        }
    }

    public class OutQuart
    {
        public static float Calc(float v)
        {
            var f = (v - 1f);
            return f * f * f * (1f - v) + 1f;
        }
    }

    public class InOutQuart
    {
        public static float Calc(float v)
        {
            if (v < 0.5f)
            {
                return 8f * v * v * v * v;
            }
            else
            {
                var f = ((2f * v) - 2f);
                return 0.5f * f * f * f * f + 1f;
            }
        }
    }

    public class InQuint
    {
        public static float Calc(float v)
        {
            return v * v * v * v * v;
        }
    }

    public class OutQuint
    {
        public static float Calc(float v)
        {
            var f = (v - 1f);
            return f * f * f * f * f + 1f;
        }
    }

    public class InOutQuint
    {
        public static float Calc(float v)
        {
            if (v < 0.5f)
            {
                return 16f * v * v * v * v * v;
            }
            else
            {
                var f = ((2f * v) - 2f);
                return 0.5f * f * f * f * f * f + 1f;
            }
        }
    }

    public class InSine
    {
        public static float Calc(float v)
        {
            return Mathf.Sin((v - 1f) * (Mathf.PI / 2f)) + 1f;
        }
    }

    public class OutSine
    {
        public static float Calc(float v)
        {
            return Mathf.Sin(v * (Mathf.PI / 2f));
        }
    }

    public class InOutSine
    {
        public static float Calc(float v)
        {
            return 0.5f * (1f - Mathf.Cos(v * Mathf.PI));
        }
    }
}
