using UnityEngine;

namespace Application.Contexts.GameplayContext.Models
{
    public class PlayerInputModel
    {
        public Vector2 Mouse;
        public Vector2 Point;
        public Vector2 Movement;
        public bool RightClick;
        public bool MiddleClick;
        public bool SpaceClick;
        public float Scroll;
    }
}