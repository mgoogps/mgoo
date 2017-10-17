function GetCoureName(course) {
    var name = "";
    course = parseFloat(course);
    if ((course >= 0 && course < 22.5) || (course >= 337.5 && course < 360)) // 0
    {
        name = courseName.dueNorth;
    }
    else if (course >= 22.5 && course < 67.5) // 45
    {
        name = courseName.northeast;
    }
    else if (course >= 67.5 && course < 112.5) // 90
    {
        name = courseName.dueEast;
    }
    else if (course >= 112.5 && course < 157.5) //135
    {
        name = courseName.southeast;
    }
    else if (course >= 157.5 && course < 202.5) //180
    {
        name = courseName.dueSouth;
    }
    else if (course >= 202.5 && course < 247.5) //225
    {
        name = courseName.southwest;
    }
    else if (course >= 247.5 && course < 292.5) //270
    {
        name = courseName.dueWest;
    }
    else if (course >= 292.5 && course < 337.5) //315
    {
        name = courseName.northwest;
    }
    else {
        name = "";
    }
    return name;
}

//只用于百度地图
function GetBaiduIcon(t, s, c) {
    if (s == "Arrears") {
        s = "Offline";
    }
    var bIcon = new BMap.Icon("/Scripts/icons/carIcon/27_0.png", new BMap.Size(12, 20));
    var course = parseFloat(c);
    t = parseInt(t);
    var icon = "";
    if ((course >= 0 && course < 22.5) || (course >= 337.5 && course < 360) || course >= 360) // 0,360
    {
        if (t == 1) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_27_0.png";
            } else {
                icon = "/Scripts/icons/carIcon/27_0.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 2) {
            icon = "/Scripts/icons/carIcon/2.png";
            bIcon = new BMap.Icon(icon, new BMap.Size(14, 18));
        } else if (t == 21) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_27_0.png";
            } else {
                icon = "/Scripts/icons/carIcon/21_0.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 22) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_22_0.png";
            } else {
                icon = "/Scripts/icons/carIcon/22_0.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 23) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_23_0.png";
            } else {
                icon = "/Scripts/icons/carIcon/23_0.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(24, 24));
        } else if (t == 24) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_24_0.png";
            } else {
                icon = "/Scripts/icons/carIcon/24_0.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 25) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_25_0.png";
            } else {
                icon = "/Scripts/icons/carIcon/25_0.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 26) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_26_0.png";
            } else {
                icon = "/Scripts/icons/carIcon/26_0.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        }
    }
    else if (course >= 22.5 && course < 67.5) // 45
    {
        if (t == 1) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_27_45.png";
            } else {
                icon = "/Scripts/icons/carIcon/27_45.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 2) {
            icon = "/Scripts/icons/carIcon/2.png";
            bIcon = new BMap.Icon(icon, new BMap.Size(14, 18));
        } else if (t == 21) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_27_45.png";
            } else {
                icon = "/Scripts/icons/carIcon/21_45.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 22) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_22_45.png";
            } else {
                icon = "/Scripts/icons/carIcon/22_45.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 23) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_23_45.png";
            } else {
                icon = "/Scripts/icons/carIcon/23_45.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(24, 24));
        } else if (t == 24) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_24_45.png";
            } else {
                icon = "/Scripts/icons/carIcon/24_45.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 25) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_25_45.png";
            } else {
                icon = "/Scripts/icons/carIcon/25_45.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 26) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_26_45.png";
            } else {
                icon = "/Scripts/icons/carIcon/26_45.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        }
    }
    else if (course >= 67.5 && course < 112.5) // 90
    {
        if (t == 1) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_27_90.png";
            } else {
                icon = "/Scripts/icons/carIcon/27_90.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 2) {
            icon = "/Scripts/icons/carIcon/2.png";
            bIcon = new BMap.Icon(icon, new BMap.Size(14, 18));
        } else if (t == 21) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_27_90.png";
            } else {
                icon = "/Scripts/icons/carIcon/21_90.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 22) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_22_90.png";
            } else {
                icon = "/Scripts/icons/carIcon/22_90.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 23) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_23_90.png";
            } else {
                icon = "/Scripts/icons/carIcon/23_90.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(24, 24));
        } else if (t == 24) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_24_90.png";
            } else {
                icon = "/Scripts/icons/carIcon/24_90.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 25) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_25_90.png";
            } else {
                icon = "/Scripts/icons/carIcon/25_90.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 26) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_26_90.png";
            } else {
                icon = "/Scripts/icons/carIcon/26_90.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        }
    }
    else if (course >= 112.5 && course < 157.5) //135
    {
        if (t == 1) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_27_135.png";
            } else {
                icon = "/Scripts/icons/carIcon/27_135.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 2) {
            icon = "/Scripts/icons/carIcon/2.png";
            bIcon = new BMap.Icon(icon, new BMap.Size(14, 18));
        } else if (t == 21) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_27_135.png";
            } else {
                icon = "/Scripts/icons/carIcon/21_135.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 22) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_22_135.png";
            } else {
                icon = "/Scripts/icons/carIcon/22_135.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 23) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_23_135.png";
            } else {
                icon = "/Scripts/icons/carIcon/23_135.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(24, 24));
        } else if (t == 24) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_24_135.png";
            } else {
                icon = "/Scripts/icons/carIcon/24_135.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 25) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_25_135.png";
            } else {
                icon = "/Scripts/icons/carIcon/25_135.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 26) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_26_135.png";
            } else {
                icon = "/Scripts/icons/carIcon/26_135.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        }
    }
    else if (course >= 157.5 && course < 202.5) //180
    {
        if (t == 1) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_27_180.png";
            } else {
                icon = "/Scripts/icons/carIcon/27_180.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 2) {
            icon = "/Scripts/icons/carIcon/2.png";
            bIcon = new BMap.Icon(icon, new BMap.Size(14, 18));
        } else if (t == 21) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_27_180.png";
            } else {
                icon = "/Scripts/icons/carIcon/21_180.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 22) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_22_180.png";
            } else {
                icon = "/Scripts/icons/carIcon/22_180.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 23) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_23_180.png";
            } else {
                icon = "/Scripts/icons/carIcon/23_180.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(24, 24));
        } else if (t == 24) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_24_180.png";
            } else {
                icon = "/Scripts/icons/carIcon/24_180.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 25) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_25_180.png";
            } else {
                icon = "/Scripts/icons/carIcon/25_180.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 26) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_26_180.png";
            } else {
                icon = "/Scripts/icons/carIcon/26_180.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        }
    }
    else if (course >= 202.5 && course < 247.5) //225
    {
        if (t == 1) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_27_225.png";
            } else {
                icon = "/Scripts/icons/carIcon/27_225.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 2) {
            icon = "/Scripts/icons/carIcon/2.png";
            bIcon = new BMap.Icon(icon, new BMap.Size(14, 18));
        } else if (t == 21) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_27_225.png";
            } else {
                icon = "/Scripts/icons/carIcon/21_225.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 22) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_22_225.png";
            } else {
                icon = "/Scripts/icons/carIcon/22_225.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 23) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_23_225.png";
            } else {
                icon = "/Scripts/icons/carIcon/23_225.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(24, 24));
        } else if (t == 24) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_24_225.png";
            } else {
                icon = "/Scripts/icons/carIcon/24_225.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 25) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_25_225.png";
            } else {
                icon = "/Scripts/icons/carIcon/25_225.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 26) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_26_225.png";
            } else {
                icon = "/Scripts/icons/carIcon/26_225.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        }
    }
    else if (course >= 247.5 && course < 292.5) //270
    {
        if (t == 1) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_27_270.png";
            } else {
                icon = "/Scripts/icons/carIcon/27_270.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 2) {
            icon = "/Scripts/icons/carIcon/2.png";
            bIcon = new BMap.Icon(icon, new BMap.Size(14, 18));
        } else if (t == 21) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_27_270.png";
            } else {
                icon = "/Scripts/icons/carIcon/21_270.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 22) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_22_270.png";
            } else {
                icon = "/Scripts/icons/carIcon/22_270.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 23) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_23_270.png";
            } else {
                icon = "/Scripts/icons/carIcon/23_270.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(24, 24));
        } else if (t == 24) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_24_270.png";
            } else {
                icon = "/Scripts/icons/carIcon/24_270.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 25) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_25_270.png";
            } else {
                icon = "/Scripts/icons/carIcon/25_270.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 26) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_26_270.png";
            } else {
                icon = "/Scripts/icons/carIcon/26_270.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        }
    }
    else if (course >= 292.5 && course < 337.5) //315
    {
        if (t == 1) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_27_315.png";
            } else {
                icon = "/Scripts/icons/carIcon/27_315.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 2) {
            icon = "/Scripts/icons/carIcon/2.png";
            bIcon = new BMap.Icon(icon, new BMap.Size(14, 18));
        } else if (t == 21) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_27_315.png";
            } else {
                icon = "/Scripts/icons/carIcon/21_315.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 22) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_22_315.png";
            } else {
                icon = "/Scripts/icons/carIcon/22_315.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 23) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_23_315.png";
            } else {
                icon = "/Scripts/icons/carIcon/23_315.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(24, 24));
        } else if (t == 24) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_24_315.png";
            } else {
                icon = "/Scripts/icons/carIcon/24_315.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 25) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_25_315.png";
            } else {
                icon = "/Scripts/icons/carIcon/25_315.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        } else if (t == 26) {
            if (s == "Offline") {
                icon = "/Scripts/icons/carIcon/offline_26_315.png";
            } else {
                icon = "/Scripts/icons/carIcon/26_315.png";
            }
            bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
        }
    }
    else {

    }
    if (t == 30) {
        if (s == "Offline") {
            icon = "/Scripts/icons/carIcon/offline_30.png";
        } else {
            icon = "/Scripts/icons/carIcon/30.png";
        }
        bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
    } else if (t == 31) {
        if (s == "Offline") {
            icon = "/Scripts/icons/carIcon/offline_31.png";
        } else {
            icon = "/Scripts/icons/carIcon/31.png";
        }
        bIcon = new BMap.Icon(icon, new BMap.Size(28, 28));
    }

    return bIcon;
}
