namespace CardHouse
{
    public static class Utils
    {
        public static float CorrectAngle(float angle) // yields an angle beween -180 and 180
        {
            while (angle < 0)
            {
                angle += 360;
            }
            while (angle > 360)
            {
                angle -= 360;
            }
            return angle;
        }
    }
}