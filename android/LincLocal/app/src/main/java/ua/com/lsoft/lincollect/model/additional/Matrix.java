package ua.com.lsoft.lincollect.model.additional;

import java.util.ArrayList;
import java.util.Arrays;

import ua.com.lsoft.lincollect.model.main.Answer;

/**
 * Created by Evgeniy on 06.06.2016.
 */
public class Matrix {

    private long id;
    private ArrayList<Answer> columns;
    private String[] rows;

    public Matrix() {
    }

    public Matrix(long id, ArrayList<Answer> columns, String[] rows) {
        this.id = id;
        this.columns = columns;
        this.rows = rows;
    }

    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    public ArrayList<Answer> getColumns() {
        return columns;
    }

    public void setColumns(ArrayList<Answer> columns) {
        this.columns = columns;
    }

    public String[] getRows() {
        return rows;
    }

    public void setRows(String[] rows) {
        this.rows = rows;
    }

    @Override
    public String toString() {
        return "Matrix{" +
                "id=" + id +
                ", columns=" + columns +
                ", rows=" + Arrays.toString(rows) +
                '}';
    }
}
