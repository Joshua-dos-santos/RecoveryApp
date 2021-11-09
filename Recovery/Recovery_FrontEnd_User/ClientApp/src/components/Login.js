import React, { Component, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { Input } from 'reactstrap';
import Button from 'reactstrap/lib/Button';
import ButtonGroup from 'reactstrap/lib/ButtonGroup';
import Card from 'reactstrap/lib/Card';
import CardBody from 'reactstrap/lib/CardBody';
import CardFooter from 'reactstrap/lib/CardFooter';
import CardImg from 'reactstrap/lib/CardImg';
import CardTitle from 'reactstrap/lib/CardTitle';
import Form from 'reactstrap/lib/Form';
import Label from 'reactstrap/lib/Label';
import { NavMenu } from '../components/NavMenu';
import "./Login.css";
import axios from 'axios'
import ReactSession from 'react-client-session/dist/ReactSession';
import { Redirect } from 'react-router-dom';

export class Login extends Component {
    static displayName = Login.name;
    constructor(props) {
        super(props)

        this.state = {
            email: '',
            password: '',
            token: '',
            hasError: false,
            errorMessage: '',
            loggedIn: false,
            token: null
        }
        this.handleLogin = this.handleLogin.bind(this)
    }

    componentDidUpdate() {
    }

    setSession = (token) => {
        ReactSession.set("loggedin", true);
        ReactSession.set("token", token.data);

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
            url: 'https://localhost:44307/Account/Login/Login',
            data: { email, password }
        }).then(token => this.setSession(token)).catch(function (error) {
            if (error.message == "Request failed with status code 401") {
                self.setState({ hasError: true, errorMessage: "Email or Password is incorrect" })
            }
            return Promise.reject(error)
        });
    }
    render() {
        if (ReactSession.get("loggedin")) {
            return (
                <Redirect to="/user/dashboard/" />
            )

        }

        return (
            <>
                <NavMenu />
                <div className="row p-0 mx-auto logincontainer">
                    <div className="col-12 col-lg-6 p-1">
                        <h1 className="text-center">Login</h1>
                        <div className="col-12">
                            <Form>
                                {this.state.hasError ? (
                                    <div className="py-2 col-12">
                                        <Label className="alert alert-danger col-12" role="alert">{this.state.errorMessage}</Label>
                                    </div>
                                ) : (<></>)
                                }
                                <div className="py-2">
                                    <Label for="email">Email</Label>
                                    <Input type="text" onChange={(e) => this.setState({ mail: e.target.value })} name="email" />
                                </div>
                                <div className="py-2">
                                    <Label for="password">Password</Label>
                                    <Input type="password" onChange={(e) => this.setState({ password: e.target.value })} name="password" />
                                </div>
                                <div className="py-2">
                                    <Button className="my-2 mr-2 ml-0 loginbutton" onClick={(e) => this.handleLogin(e)}>Login</Button>
                                    <Link className="m-2 registerlink" to="/register">No account yet? Register here!</Link>
                                </div>
                            </Form>
                        </div>
                    </div>
                </div>
            </>
        );
    }
}

