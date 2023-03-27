﻿using CI_PLATFORM.Entities.DataModels;
using CI_PLATFORM.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM_.repository.Interface
{
    public interface IVolunterstoryInterface
    {
        public StoryViewModel Getstorylist(int pageIndex);

        public storydetailviewmodel GetStory(int id);

        public List<Mission> GetMissions(long userid);

        public void addstory(long missionId, string title, DateTime date, string videoURL, string description, string[] imagePaths, long userid);

        public AddStoryViewmodel getData(long userid);

      }
}
