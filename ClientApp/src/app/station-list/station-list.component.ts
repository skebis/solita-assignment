import { AfterViewInit, Component, Inject, Injectable, OnDestroy, OnInit, ViewChild } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { MatPaginator, PageEvent } from "@angular/material/paginator";
import { MatTableDataSource } from "@angular/material/table";
import { Router } from "@angular/router";
import { Station } from "../classes/station";
import { JourneyService } from "../journey-service";

@Component({
  selector: 'app-station-list',
  templateUrl: './station-list.component.html',
  styleUrls: ['./station-list.component.css'],
  providers: [JourneyService]
})

@Injectable()
export class StationListComponent implements OnInit, OnDestroy, AfterViewInit {
  stations: any[] = [];
  loading: boolean = true;

  dataSource = new MatTableDataSource<any>();

  // Mat table definitions
  displayedColumns: string[] = ['name', 'address', 'actions'];

  @ViewChild(MatPaginator)
  paginator!: MatPaginator;

  constructor(private journeyService: JourneyService, private router: Router) {

  }

  ngAfterViewInit() {
    this.showStations();
  }

  ngOnInit(): void {
  }

  ngOnDestroy() {
    // Unsubscribes here
  }

  ngOnChanges() {
    // Change detection things here
  }

  openStationDetails(station: Station): void {
    this.router.navigateByUrl('/station/' + station.stationId);
  }

  showStations() {
    this.journeyService.getStations()
      .subscribe(res => {
        this.loading = false;
        this.stations = res;
        this.dataSource = new MatTableDataSource<any>(this.stations);
        this.dataSource.paginator = this.paginator;
      });
  }

  pageChanged(event: any) {
    this.loading = true;
    this.showStations();
  }

  /*showJourney(id: string) {
    this.journeyService.getJourney(id).subscribe(res => {
      this.journeys = res;
    });
  }*/
}
