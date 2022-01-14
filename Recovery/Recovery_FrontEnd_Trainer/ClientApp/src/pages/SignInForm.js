import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import "./Login.css";
import axios from 'axios'
import ReactSession from 'react-client-session/dist/ReactSession';
import { Redirect } from 'react-router-dom';
import { Card } from 'reactstrap';


export default class SignInForm extends Component {
    static displayName = SignInForm.name;
    constructor(props) {
        super(props)
        this.state = {
            mail: '',
            password: '',
            token: '',
            hasError: false,
            errorMessage: '',
            loggedIn: false,
            token: null
        }
        this.handleChange = this.handleChange.bind(this);
        this.handleLogin = this.handleLogin.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleSubmit(event) {
        event.preventDefault();

        console.log("The form was submitted with the following data:");
        console.log(this.state);
    }

    handleChange(event) {
        let target = event.target;
        let value = target.type === "checkbox" ? target.checked : target.value;
        let name = target.name;

        this.setState({
            [name]: value
        });
    }

    componentDidUpdate() {
    }

    setSession = (token) => {
        sessionStorage.setItem("loggedin", true);
        sessionStorage.setItem("token", token.data);
        sessionStorage.setItem("Key", token);
        this.setState({ token: token.data, loggedIn: true });
    }

    handleLogin = (event) => {
        event.preventDefault();
        const email = this.state.mail
        const password = this.state.password

        this.setState({ hasError: false, errorMessage: '' })

        if (email == '' || email == null) {
            this.setState({ hasError: true, errorMessage: "Email must be filled in!" })
            return;
        } else if (password == '' || password == null) {
            this.setState({ hasError: true, errorMessage: "Password must be filled in!" })
            return;
        }

        var self = this;
        axios({
            method: 'post',
            url: 'http://localhost:5000/PT/LoginPT/Login',
            data: { email, password }
        }).then(token => this.setSession(token)).catch(function (error) {
            if (error.message == "Request failed with status code 401") {
                self.setState({ hasError: true, errorMessage: "Email or Password is incorrect" })
            }

            return Promise.reject(error)
        });
    }
    render() {
        if (sessionStorage.getItem("loggedin")) {
            return (
                <Redirect exact to="/" />
            )

        }

        return (
            <div className="formCenter"> 
                <form className="formFields" onSubmit={this.handleSubmit}>
                    <div className="formField">
                        <label className="formFieldLabel" htmlFor="email">
                            E-Mail Address
                        </label>
                        <input
                            type="text"
                            id="email"
                            className="formFieldInput"
                            placeholder="Enter your email"
                            name="email"
                            value={this.state.email}
                            onChange={(e) => this.setState({ mail: e.target.value })}
                        />
                    </div>

                    <div className="formField">
                        <label className="formFieldLabel" htmlFor="password">
                            Password
                        </label>
                        <input
                            type="password"
                            id="password"
                            className="formFieldInput"
                            placeholder="Enter your password"
                            name="password"
                            value={this.state.password}
                            onChange={(e) => this.setState({ password: e.target.value })}
                        />
                    </div>

                    <div className="formField">
                        <a href="https://localhost:5000/login">
                            <button className="formFieldButton" onClick={(e) => { this.handleLogin(e); window.location.reload(false); }}>Sign In</button>
                        </a>
                        <Link className="m-2 registerlink" to="/register" id="registerLink">No account yet? Register here!</Link>
                    </div>
                </form>
            </div>
        );
    }

}