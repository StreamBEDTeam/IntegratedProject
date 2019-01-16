public class Axis2DToPress
{
    private float cutoff;
    private int state;
    private OVRInput.Axis2D axis;
    private OVRInput.Controller controller;
    public Axis2DToPress(OVRInput.Axis2D axis, OVRInput.Controller controller, float cutoff)
    {
        this.axis = axis;
        this.controller = controller;
        this.cutoff = cutoff;
        state = 0;
    }
    public int Get()
    {
        var current = OVRInput.Get(axis, controller).y;//, OVRInput.Controller.RTouch);
        if (current > cutoff)
        {
            if (state == 0)
            {
                state = 1;
                return 1;
            }
        }
        else
        {
            if (current < -cutoff)
            {
                if (state == 0)
                {
                    state = -1;
                    return -1;
                }
            }
            else
            {
                state = 0;
            }
        }
        return 0;
    }

}