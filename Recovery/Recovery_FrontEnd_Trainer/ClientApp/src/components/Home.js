import React, { Component } from 'react';
import ReactFlexyTable from "react-flexy-table";
import "react-flexy-table/dist/index.css"
import deleteIcon from "./Icons/Delete.png"
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
        this.handleSubmit = this.handleSubmit.bind(this);
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
                jtoken: sessionStorage.getItem("token")
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

    handleSubmit = (userId) => {
        var self = this;
        const userID = userId
        console.log(userID)
        axios({
            method: 'DELETE',
            url: 'http://localhost:5000/PT/DeleteUser/DeleteUser',
            params: {
                userId: userID
            }
        }).then(function (data) {
            console.log(data.data)
            window.location.reload(false)
        });
    }

    render() {
        if (!sessionStorage.getItem("loggedin")) {
            return (
                <Redirect to="/login" />
            )
        }
        const additionalCols = [{
            header: "",
            td: (data) => {
                return <div>
                    <img src={deleteIcon} width="30" height="20" onClick={(e) => this.handleSubmit(data.unique_ID)} /> 
                </div>
            }
        }]
        return (
            <ReactFlexyTable data={this.state.users} sortable nonSortableCols={["name", "email"]} filterable nonFilterCols={["height", "weight", "birthdate"]} additionalCols={additionalCols }/>
        );
    }
}
