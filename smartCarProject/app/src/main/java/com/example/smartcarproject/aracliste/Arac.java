package com.example.smartcarproject.aracliste;

public class Arac {
    private String aracPlaka;
    private int aracId;

    public Arac(String aracPlaka, int aracId) {
        this.aracPlaka = aracPlaka;
        this.aracId = aracId;
    }

    public int getAracId() {
        return aracId;
    }

    public void setAracId(int aracId) {
        this.aracId = aracId;
    }

    public Arac(String aracPlaka) {
        this.aracPlaka = aracPlaka;
    }

    public String getAracPlaka() {
        return aracPlaka;
    }

    public void setAracPlaka(String aracPlaka) {
        this.aracPlaka = aracPlaka;
    }
}
