using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Test2
{
    static void Main(string[] args)
    {
        GameManager.CreateHouse(new RomanBuilder());
    }
}
public abstract class House
{

}
interface Door
{
    void BuildDoor();
}
interface Wall
{
    void BuildWall();
}
interface Windows
{
    void BuildWindows();
}
interface Floor
{
    void BuildFloor();
}
interface HouseCeiling
{
    void BuildHouseCeiling();
}
public class RomanDoor : Door
{
    public void BuildDoor() { }
}
public class RomanWall : Wall
{
    public void BuildWall() { }
}
public class RomanWindows : Windows
{
    public void BuildWindows() { }
}
public class RomanFloor : Floor
{
    public void BuildFloor() { }
}
public class RomanHouseCeiling : HouseCeiling
{
    public void BuildHouseCeiling() {}
}
public abstract class Builder:Door,Wall,Windows,Floor,HouseCeiling
{

    public abstract House GetHouse();
    public abstract void BuildDoor();
    public abstract void BuildWall();
    public abstract void BuildWindows();
    public abstract void BuildFloor();
    public abstract void BuildHouseCeiling();
   
}
public class RomanHouse : House
{

}
public class RomanBuilder : Builder
{
    private RomanDoor rmDoor
    {
        get;
        set;
    }
    private RomanWall rmWall
    {
        get;
        set;
    }
    private RomanWindows rmWindows
    {
        get;
        set;
    }
    private RomanFloor rmFloor
    {
        get;
        set;
    }
    private RomanHouseCeiling rmHouseCeiling
    {
        get;
        set;
    }

    public override void BuildDoor()
    {
        rmDoor.BuildDoor();
    }

    public override void BuildFloor()
    {
        rmFloor.BuildFloor();
    }

    public override void BuildHouseCeiling()
    {
        rmHouseCeiling.BuildHouseCeiling();
    }

    public override void BuildWall()
    {
        rmWall.BuildWall();
    }

    public override void BuildWindows()
    {
        rmWindows.BuildWindows();
    }

    public override House GetHouse()
    {
        return new RomanHouse();
    }
}
public class GameManager//客户端
{
    public static House CreateHouse(Builder builder)
    {
        //这个方法内部去创造房子的部件
        builder.BuildDoor();
        builder.BuildDoor();

        builder.BuildWindows();
        builder.BuildWindows();

        builder.BuildWall();
        builder.BuildWall();


        builder.BuildFloor();
        builder.BuildHouseCeiling();

        return builder.GetHouse();
    }
}



