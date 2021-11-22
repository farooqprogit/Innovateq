import { Component, OnInit } from '@angular/core';
import {UserService} from '../../services/user.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {
  errorMessage:any;
  blogs:[]=[];
  users:[]=[];
  tabdescription:any="Home Page Description";
  tabname:any="Home";
  isHometab:boolean = true;
  constructor(private userService:UserService) { }

  ngOnInit() {
    this.getBlogs();
  }
  getBlogs()
  {
    this.userService.getBlogs().subscribe(
      (data) => {
          if (data) {
              this.blogs = data;
          }
      },
      (error) => {
          this.errorMessage = error;
      },
      () => {
      });
  }
  getUsers()
  {
    this.userService.getUsers().subscribe(
      (data) => {
          if (data) {
              this.users = data;
              this.tabdescription="Total Users: " + this.users.length;
          }
      },
      (error) => {
          this.errorMessage = error;
      },
      () => {
      });
  }
  onTabClick(tab){
  if(tab=="home")
  {
    this.isHometab=true;
    this.tabname="Home";
    this.tabdescription="Home Page Description"
    this.getBlogs();
  }
  else
  {
    this.isHometab=false;
    this.tabname="Users";
    this.getUsers();
  }
  }
}
