using UnityEngine;
using System.Collections;

public class ShapesRenderer
{
	public delegate void SetPixelDelegate(Point2D p, Color color);
	
	public static void DrawLine(Texture2D tex, Point2D p1, Point2D p2, Color color, SetPixelDelegate setPixel)
	{
		int x1 = p1.X;
		int y1 = p1.Y;
		int x2 = p2.X;
		int y2 = p2.Y;
		
		// zmienne pomocnicze
     	int d, dx, dy, ai, bi, xi, yi;
     	int x = x1, y = y1;
     	// ustalenie kierunku rysowania
     	if (x1 < x2)
     	{ 
         	xi = 1;
         	dx = x2 - x1;
     	} 
     	else
	    { 
	        xi = -1;
	        dx = x1 - x2;
	    }
	    // ustalenie kierunku rysowania
	    if (y1 < y2)
	    { 
	        yi = 1;
	        dy = y2 - y1;
	    } 
	    else
	    { 
	        yi = -1;
	        dy = y1 - y2;
	    }
	    // pierwszy piksel
		if (x >= tex.width || x < 0 || y >= tex.height || y < 0)
			return;
	    setPixel(new Point2D(x, y), color);
	    // oś wiodąca OX
	    if (dx > dy)
	    {
	        ai = (dy - dx) * 2;
	        bi = dy * 2;
	        d = bi - dx;
	        // pętla po kolejnych x
	        while (x != x2)
	        { 
	            // test współczynnika
	            if (d >= 0)
	            { 
	                x += xi;
	                y += yi;
	                d += ai;
	            } 
	            else
	            {
	                d += bi;
	                x += xi;
	            }
				if (x >= tex.width || x < 0 || y >= tex.height || y < 0)
					return;
	            setPixel(new Point2D(x, y), color);
	        }
	    } 
	    // oś wiodąca OY
	    else
	    { 
	        ai = ( dx - dy ) * 2;
	        bi = dx * 2;
	        d = bi - dy;
	        // pętla po kolejnych y
	        while (y != y2)
	        { 
	            // test współczynnika
	            if (d >= 0)
	            { 
	                x += xi;
	                y += yi;
	                d += ai;
	            }
	            else
	            {
	                d += bi;
	                y += yi;
	            }
				if (x >= tex.width || x < 0 || y >= tex.height || y < 0)
					return;
	            setPixel(new Point2D(x, y), color);
	        }
	    }
	}
}
