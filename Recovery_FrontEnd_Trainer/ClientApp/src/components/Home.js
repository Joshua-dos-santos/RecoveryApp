import React, { Component } from 'react';
import ReactFlexyTable from "react-flexy-table";
import axios from 'axios'
import { Redirect } from 'react-router-dom';

export class Home extends Component {
    static displayName = Home.name;
    constructor(props) {
        super(props);
        this.state = {
            users: [],
            trainer: [],
            jtoken: localStorage.getItem("token"),
            loggedIn: false
        };
        this.OnLoad = this.OnLoad.bind(this);
        this.populateData = this.populateData.bind(this);
    }

    componentDidMount() {
        this.OnLoad();
    }

    OnLoad(e) {
        var self = this;
        console.log(localStorage.getItem("token"))
        axios({
            method: 'GET',
            url: 'http://localhost:5000/PT/GetPTByToken/GetPTByToken',
            params: {
                jtoken: localStorage.getItem("token")
            }
        }).then((data) => {
            console.log(data);
            self.setState({ trainer: data.data }, () => { console.log(self.state.trainer) });
            self.populateData();
        });
    }

    populateData = async () => {
        var self = this;
        var id = self.state.trainer.unique_ID
        console.log(id);
        axios({
            method: 'get',
            url: 'http://localhost:5000/PT/GetAllUsers/GetAllUsers',
            dataType: "json",
            params: {
                ptid: id
            }
        }).then(function (data) {
            console.log(data.data);
            self.setState({ users: data.data, loading: false });
        }
        );
    }

    render() {
        if (!localStorage.getItem("loggedin")) {
            return (
                <Redirect to="/login" />
            )
        }
    return (
      <div>
            <h1>Hello, world!</h1>
            <button class="btn btn-primary" onClick={(e) => this.populateData()}>test</button>
        <p>Welcome to your new single-page application, built with:</p>
        <ul>
          <li><a href='https://get.asp.net/'>ASP.NET Core</a> and <a href='https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx'>C#</a> for cross-platform server-side code</li>
          <li><a href='https://facebook.github.io/react/'>React</a> for client-side code</li>
          <li><a href='http://getbootstrap.com/'>Bootstrap</a> for layout and styling</li>
        </ul>
        <p>To help you get started, we have also set up:</p>
        <ul>
          <li><strong>Client-side navigation</strong>. For example, click <em>Counter</em> then <em>Back</em> to return here.</li>
          <li><strong>Development server integration</strong>. In development mode, the development server from <code>create-react-app</code> runs in the background automatically, so your client-side resources are dynamically built on demand and the page refreshes when you modify any file.</li>
          <li><strong>Efficient production builds</strong>. In production mode, development-time features are disabled, and your <code>dotnet publish</code> configuration produces minified, efficiently bundled JavaScript files.</li>
        </ul>
        <p>The <code>ClientApp</code> subdirectory is a standard React application based on the <code>create-react-app</code> template. If you open a command prompt in that directory, you can run <code>npm</code> commands such as <code>npm test</code> or <code>npm install</code>.</p>
      </div>
    );
  }
}
