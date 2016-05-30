package com.example.notebookuser.customremote;

import android.app.Dialog;
import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.EditText;

import java.net.InetAddress;
import java.net.UnknownHostException;

public class ChangeAddressActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_change_address);

        EditText edit_address = (EditText)findViewById(R.id.edit_address);
        EditText edit_port = (EditText)findViewById(R.id.edit_port);

        try {
            edit_address.setText(new Statics().getAddress());
            edit_port.setText(new Statics().getPort());
        }
        catch (Exception e)
        {
            Dialog d = new Dialog(this);
            d.setTitle(e.getMessage());
            d.show();
        }
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_change_address, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }

    public void btClick(View view)
    {
        EditText editAddress = (EditText) findViewById(R.id.edit_address);
        EditText editPort = (EditText) findViewById(R.id.edit_port);
        try {
            InetAddress address = InetAddress.getByName(editAddress.getText().toString());
            Statics statics = new Statics();
            statics.setAddress(editAddress.getText().toString());
            statics.setPort(Integer.parseInt(editPort.getText().toString()));
            Intent intent = new Intent(this, MainActivity.class);
            startActivity(intent);
        }
        catch (UnknownHostException e)
        {
            Dialog d = new Dialog(this);
            d.setTitle(R.string.address_parse_error);
            d.show();
        }
    }
}
