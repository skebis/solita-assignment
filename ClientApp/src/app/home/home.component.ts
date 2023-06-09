import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { AddJourneyComponent } from '../add-journey/add-journey.component';
import { AddStationComponent } from '../add-station/add-station.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  constructor(public dialog: MatDialog, public router: Router) {

  }

  openAddJourneyDialog(): void {
    const dialogRef = this.dialog.open(AddJourneyComponent, {
      
    });

    dialogRef.afterClosed().subscribe(() => {
      console.log('dialog closed');
    });
  }

  openAddStationDialog(): void {
    const dialogRef = this.dialog.open(AddStationComponent, {

    });

    dialogRef.afterClosed().subscribe(() => {
      console.log('dialog closed');
    });
  }

  openJourneyList() {
    this.router.navigateByUrl("/journeys");
  }

  openStationList() {
    this.router.navigateByUrl("/stations");
  }
}
