import React, { Component } from 'react';
import ReactFlexyTable from "react-flexy-table";
import "react-flexy-table/dist/index.css"
import axios from 'axios'
import { Redirect } from 'react-router-dom';

export class Home extends Component {
    static displayName = Home.name;
    constructor(props) {
        super(props);
        this.state = {
            users: [],
            injuries: [],
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
            self.setState({ users: data.data.users, inuries: data.data.injuries, loading: false });
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
                <table className='table table-striped' aria-labelledby="tabelLabel" id="meals">
                    <thead>
                        <tr>
                            <th>First Name</th>
                            <th>Last Name</th>
                            <th>Injury</th>
                            <th>Weight</th>
                            <th>Height</th>
                            <th>Birthdate</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.users.map(user =>
                            <tr key={user.unique_ID}>
                                <td>{user.first_Name}</td>
                                <td>{user.last_Name}</td>
                                {this.state.injuries.map(injury =>
                                    <td>{injury.description}</td>
                                )}
                                <td>{user.weight}</td>
                                <td>{user.height}</td>
                                <td>{user.birthdate}</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        );
    }
}
