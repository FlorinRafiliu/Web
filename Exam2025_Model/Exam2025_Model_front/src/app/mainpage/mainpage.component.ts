import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'mainpage',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './mainpage.component.html',
  styleUrls: ['./mainpage.component.css']
})
export class MainpageComponent {
    name: string = "";
    skill: string = "";
    developer: string = "";
    projectsName: string = "";

    nameIsInserterd: boolean = false;
    onlyMine: boolean = false;

    errorMessage: string = "";

    projects: any[] = [];
    developers: any[] = [];

    constructor(private http: HttpClient, private router: Router) {}

    ngOnInit() {
        this.name = localStorage.getItem("userId") ?? "";
        this.loadData();
    }

    assignProjects() {
        if(this.developer == '')
            return;
        if(this.projectsName == '')
            return;

        const body = {
            developer: this.developer,
            projectsName: this.projectsName,
        }

        this.http.post("https://localhost:7260/projects", body, { responseType: 'text'} ).subscribe({
                next: (data) => {
                    if(data == "success") {
                        console.log(data);
                    } else {
                        alert(data);
                    }
                }, 
                error: (err) => {
                    console.log(err);
                }
        });
    }

    enterNameHandler() {
        if(this.name === '')
            return ;

        this.http.get<any>("https://localhost:7260/projects/id", {
            params: {
                name: this.name
            }
        }).subscribe({
                next: (data) => {
                    localStorage.setItem("userId", data);
                    console.log(data);
                    this.loadData();
                }, 
                error: (err) => {
                    console.log(err);
                }
        });
    }

    loadData() {
        if(localStorage.getItem("userId") != null) {
            this.nameIsInserterd = true;
            this.onlyMine = false;

            this.http.get<any[]>("https://localhost:7260/projects", {
                params: {
                  userId: 0
                }
            }).subscribe({
                next: (data) => {
                    this.projects = data;
                    console.log(this.projects);
                }, 
                error: (err) => {
                    console.log(err);
                }
            });
        }
    }

    loadDataById() {
        let userId = localStorage.getItem("userId");
        if(userId == null)
            return;

        this.onlyMine = true;
        this.http.get<any>("https://localhost:7260/projects", {
            params: {
                userId: userId
            }
        }).subscribe({
                next: (data) => {
                    console.log(data);
                    this.projects = data;
                }, 
                error: (err) => {
                    console.log(err);
                }
        });
    }

    getDevelopers() {
        let userId = localStorage.getItem("userId");
        if(userId == null)
            return;

        this.http.get<any>("https://localhost:7260/developers", {
            params: {
                skill: this.skill
            }
        }).subscribe({
                next: (data) => {
                    console.log(data);
                    this.developers = data;
                }, 
                error: (err) => {
                    console.log(err);
                }
        });
    }

    logout() {
        localStorage.clear();
        this.nameIsInserterd = false;
    }
}
