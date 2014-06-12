using GameSceneModel;
using UnityEngine;
using System.Collections;

namespace Utility {

    public class DiceUtility {

        /* ダイスの状態取得 */
        static public int getDiceState(int p_x, int p_z) {

            int l_state = 0;

            switch(p_x) {
                case 90:
                    l_state = (int)DiceState.FOUR;
                    break;
                case 270:
                    l_state = (int)DiceState.THREE;
                    break;
                default:

                    switch(p_z) {
                        case 0:
                            l_state = (int)DiceState.ONE;
                            break;
                        case 90:
                            l_state = (int)DiceState.TWO;
                            break;
                        case 180:
                            l_state = (int)DiceState.SIX;
                            break;
                        case 270:
                            l_state = (int)DiceState.FIVE;
                            break;
                        default :
                            l_state = (int)DiceState.None;
                            break;
                    }
                    break;
            }

            return l_state;
        }

        /* ダイスの向き取得 */
        static public int getDiceDirection(int p_y) {
            int l_direction = (int)DirectionKind.None;

            switch(p_y) {
                case 0:
                    l_direction = (int)DirectionKind.UP;
                    break;
                case 90:
                    l_direction = (int)DirectionKind.LEFT;
                    break;
                case 180:
                    l_direction = (int)DirectionKind.DOWN;
                    break;
                case 270:
                    l_direction = (int)DirectionKind.RIGHT;
                    break;
                default:
                    l_direction = (int)DirectionKind.UP;
                    break;
            }

            return l_direction;
        }

        // 1の目のベクターを取得
        static private Vector3 getVectorOne(
            int p_direct, int p_flick) {

            Vector3 l_vector;
            l_vector.x = 0;
            l_vector.y = 0;
            l_vector.z = 0;

            switch(p_direct) {

                case (int)DirectionKind.UP:
                    switch(p_flick) {
                        case (int)DirectionKind.UP:
                            l_vector.x = 10;
                            break;
                        case (int)DirectionKind.LEFT:
                            l_vector.z = 10;
                            break;
                        case (int)DirectionKind.DOWN:
                            l_vector.x = -10;
                            break;
                        case (int)DirectionKind.RIGHT:
                            l_vector.z = -10;
                            break;
                        default :
                            // NOT SET
                            break;
                    }
                    break;

                case (int)DirectionKind.LEFT:
                    switch(p_flick) {
                        case (int)DirectionKind.UP:
                            l_vector.z = 10;
                            break;
                        case (int)DirectionKind.LEFT:
                            l_vector.x = -10;
                            break;
                        case (int)DirectionKind.DOWN:
                            l_vector.z = -10;
                            break;
                        case (int)DirectionKind.RIGHT:
                            l_vector.x = 10;
                            break;
                        default :
                            // NOT SET
                            break;
                    }
                    break;

                case (int)DirectionKind.DOWN:
                    switch(p_flick) {
                        case (int)DirectionKind.UP:
                            l_vector.x = -10;
                            break;
                        case (int)DirectionKind.LEFT:
                            l_vector.z = -10;
                            break;
                        case (int)DirectionKind.DOWN:
                            l_vector.x = 10;
                            break;
                        case (int)DirectionKind.RIGHT:
                            l_vector.z = 10;
                            break;
                        default :
                            // NOT SET
                            break;
                    }
                    break;
                case (int)DirectionKind.RIGHT:
                    switch(p_flick) {
                        case (int)DirectionKind.UP:
                            l_vector.z = -10;
                            break;
                        case (int)DirectionKind.LEFT:
                            l_vector.x = 10;
                            break;
                        case (int)DirectionKind.DOWN:
                            l_vector.z = 10;
                            break;
                        case (int)DirectionKind.RIGHT:
                            l_vector.x = -10;
                            break;
                        default :
                            // NOT SET
                            break;
                    }
                    break;
                }

            return l_vector;
        }
        // 2の目のベクターを取得
        static private Vector3 getVectorTwo(
                int p_direct, int p_flick) {
            Vector3 l_vector;
            l_vector.x = 0;
            l_vector.y = 0;
            l_vector.z = 0;

            switch(p_direct) {
                case (int)DirectionKind.UP:
                    switch(p_flick) {
                        case (int)DirectionKind.UP:
                            l_vector.y = -10;
                            break;
                        case (int)DirectionKind.LEFT:
                            l_vector.z = 10;
                            break;
                        case (int)DirectionKind.DOWN:
                            l_vector.y = 10;
                            break;
                        case (int)DirectionKind.RIGHT:
                            l_vector.z = -10;
                            break;
                        default :
                            // NOT SET
                            break;
                    }
                    break;
                case (int)DirectionKind.LEFT:
                    switch(p_flick) {
                        case (int)DirectionKind.UP:
                            l_vector.z = 10;
                            break;
                        case (int)DirectionKind.LEFT:
                            l_vector.y = 10;
                            break;
                        case (int)DirectionKind.DOWN:
                            l_vector.z = -10;
                            break;
                        case (int)DirectionKind.RIGHT:
                            l_vector.y = -10;
                            break;
                        default:
                            // NOT SET
                            break;
                    }
                    break;
                case (int)DirectionKind.DOWN:
                    switch(p_flick) {
                        case (int)DirectionKind.UP:
                            l_vector.y = 10;
                            break;
                        case (int)DirectionKind.LEFT:
                            l_vector.z = -10;
                            break;
                        case (int)DirectionKind.DOWN:
                            l_vector.y = -10;
                            break;
                        case (int)DirectionKind.RIGHT:
                            l_vector.z = 10;
                            break;
                        default :
                            // NOT SET
                            break;
                    }
                    break;

                case (int)DirectionKind.RIGHT:
                    switch(p_flick) {
                        case (int)DirectionKind.UP:
                            l_vector.z = -10;
                            break;
                        case (int)DirectionKind.LEFT:
                            l_vector.y = -10;
                            break;
                        case (int)DirectionKind.DOWN:
                            l_vector.z = 10;
                            break;
                        case (int)DirectionKind.RIGHT:
                            l_vector.y = 10;
                            break;
                        default:
                            // NOT SET
                            break;
                    }
                    break;
 
                }

            return l_vector;
        }
        // 3の目のベクターを取得
        static private Vector3 getVectorThree(
                int p_direct, int p_flick) {

            Vector3 l_vector;
            l_vector.x = 0;
            l_vector.y = 0;
            l_vector.z = 0;

            switch(p_direct) {
                case (int)DirectionKind.UP:
                    switch(p_flick) {
                        case (int)DirectionKind.UP:
                            l_vector.x = 10;
                            break;
                        case (int)DirectionKind.LEFT:
                            l_vector.y = -10;
                            break;
                        case (int)DirectionKind.DOWN:
                            l_vector.x = -10;
                            break;
                        case (int)DirectionKind.RIGHT:
                            l_vector.y = 10;
                            break;
                    }
                    break;
                case (int)DirectionKind.LEFT:
                    switch(p_flick) {
                        case (int)DirectionKind.UP:
                            l_vector.y = -10;
                            break;
                        case (int)DirectionKind.LEFT:
                            l_vector.x = -10;
                            break;
                        case (int)DirectionKind.DOWN:
                            l_vector.y = 10;
                            break;
                        case (int)DirectionKind.RIGHT:
                            l_vector.x = 10;
                            break;
                    }
                    break;
                case (int)DirectionKind.DOWN:
                    switch(p_flick) {
                        case (int)DirectionKind.UP:
                            l_vector.x = -10;
                            break;
                        case (int)DirectionKind.LEFT:
                            l_vector.y = 10;
                            break;
                        case (int)DirectionKind.DOWN:
                            l_vector.x = 10;
                            break;
                        case (int)DirectionKind.RIGHT:
                            l_vector.y = -10;
                            break;
                    }
                    break;
                case (int)DirectionKind.RIGHT:
                    switch(p_flick) {
                        case (int)DirectionKind.UP:
                            l_vector.y = 10;
                            break;
                        case (int)DirectionKind.LEFT:
                            l_vector.x = 10;
                            break;
                        case (int)DirectionKind.DOWN:
                            l_vector.y = -10;
                            break;
                        case (int)DirectionKind.RIGHT:
                            l_vector.x = -10;
                            break;
                    }
                    break;
            }

            return l_vector;
        }

        // 回転方向のベクトルを取得
        static public Vector3 getRotateVector(
                int p_state, int p_direct, int p_flick) {

            Vector3 l_vector;
            l_vector.x = 0;
            l_vector.y = 0;
            l_vector.z = 0;

            if(p_state == (int)DiceState.None) return l_vector;

            switch(p_state) {
                // 1
                case (int)DiceState.ONE:
                    l_vector = getVectorOne(p_direct, p_flick);
                    break;
                // 2
                case (int)DiceState.TWO:
                    l_vector = getVectorTwo(p_direct, p_flick);
                    break;
                // 3
                case (int)DiceState.THREE:
                    l_vector = getVectorThree(p_direct, p_flick);
                    break;
                // 4
                case (int)DiceState.FOUR:
                    l_vector = getVectorThree(p_direct, p_flick);
                    l_vector.y *= -1;
                    break;
                // 5
                case (int)DiceState.FIVE:
                    l_vector = getVectorTwo(p_direct, p_flick);
                    l_vector.y *= -1;
                    break;
                // 6
                case (int)DiceState.SIX:
                    l_vector = getVectorOne(p_direct, p_flick);
                    l_vector.x *= -1;
                    l_vector.y *= -1;
                    break;
            }

            return l_vector;

        }
    }
}
