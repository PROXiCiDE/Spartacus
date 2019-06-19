using System;
using ProjectCeleste.GameFiles.XMLParser;

namespace Spartacus.Database.DBModels.ProtoAgeUnit
{
    public class ProtoUnit
    {

        public ProtoUnit(ProtoAge4XmlUnit protoUnit)
        {
            //Name = protoUnit.Name;
            //Id = protoUnit.Id;
            //Dbid = protoUnit.Dbid;
            //DisplayNameId = protoUnit.DisplayNameId;
            //EditorNameId = protoUnit.EditorNameId;
            //RolloverTextId = protoUnit.RolloverTextId;
            //ShortRolloverTextId = protoUnit.ShortRolloverTextId;
            //ObstructionRadiusX = protoUnit.ObstructionRadiusX;
            //ObstructionRadiusZ = protoUnit.ObstructionRadiusZ;
            //MaxVelocity = protoUnit.MaxVelocity;
            //MaxRunVelocity = protoUnit.MaxRunVelocity;
            //MovementType = protoUnit.MovementType;
            //Lifespan = protoUnit.Lifespan;
            //Los = protoUnit.Los;
            //SoundFile = protoUnit.SoundFile;
            //Decay = protoUnit.Decay;
            //Flag = protoUnit.Flag;
            //AnimFile = protoUnit.AnimFile;
            //VisualScale = protoUnit.VisualScale;
            //UnitType = protoUnit.UnitType;
            //ImpactType = protoUnit.ImpactType;
            //Icon = protoUnit.Icon;
            //PortraitIcon = protoUnit.PortraitIcon;
            //InitialHitpoints = protoUnit.InitialHitpoints;
            //MaxHitpoints = protoUnit.MaxHitpoints;
            //Bounty = protoUnit.Bounty;
            //InitialResource = protoUnit.InitialResource;
            //ResourceSubType = protoUnit.ResourceSubType;
            //MinimapColor = protoUnit.MinimapColor;
            //GathererLimit = protoUnit.GathererLimit;
            //TurnRate = protoUnit.TurnRate;
            //WanderDistance = protoUnit.WanderDistance;
            //UnitAiType = protoUnit.UnitAiType;
            //Event = protoUnit.Event;
            //FormationCategory = protoUnit.FormationCategory;
            //PhysicsInfo = protoUnit.PhysicsInfo;
            //SelectionPriority = protoUnit.SelectionPriority;
            //InitialUnitAiStance = protoUnit.InitialUnitAiStance;
            //Tactics = protoUnit.Tactics;
            //PopulationCapAddition = protoUnit.PopulationCapAddition;
            //PlacementFile = protoUnit.PlacementFile;
            //BuildPoints = protoUnit.BuildPoints;
            //BuildingWorkRate = protoUnit.BuildingWorkRate;
            //AllowedAge = protoUnit.AllowedAge;
            //BuilderLimit = protoUnit.BuilderLimit;
            //Contain = protoUnit.Contain;
            //Command = protoUnit.Command;
            //UnitLevel = protoUnit.UnitLevel;
            //Trait1 = protoUnit.Trait1;
            //Trait2 = protoUnit.Trait2;
            //Trait3 = protoUnit.Trait3;
            //Trait4 = protoUnit.Trait4;
            //Trait5 = protoUnit.Trait5;
            //VanTrait1 = protoUnit.VanTrait1;
            //VanTrait2 = protoUnit.VanTrait2;
            //VanTrait3 = protoUnit.VanTrait3;
            //Cost = protoUnit.Cost;
            //CarryCapacity = protoUnit.CarryCapacity;
            //RepairPoints = protoUnit.RepairPoints;
            //Tech = protoUnit.Tech;
            //IdleTimeout = protoUnit.IdleTimeout;
            //SocketUnitType = protoUnit.SocketUnitType;
            //AllowedHeightVariance = protoUnit.AllowedHeightVariance;
            //MinimapIcon = protoUnit.MinimapIcon;
            //ProjectileProtoUnit = protoUnit.ProjectileProtoUnit;
            //BuildLimit = protoUnit.BuildLimit;
            //MaxContained = protoUnit.MaxContained;
            //InputContext = protoUnit.InputContext;
            //Train = protoUnit.Train;
            //PopulationCount = protoUnit.PopulationCount;
            //CorpseDecalTime = protoUnit.CorpseDecalTime;
            //TrainPoints = protoUnit.TrainPoints;
            //AutoAttackRange = protoUnit.AutoAttackRange;
            //TurnRadius = protoUnit.TurnRadius;
            //HeightBob = protoUnit.HeightBob;
            //DesignTag = protoUnit.DesignTag;
            //DeathMessage = protoUnit.DeathMessage;
        }
        public string Name { get; set; }
        public long Id { get; set; }
        public long Dbid { get; set; }
        public long DisplayNameId { get; set; }
        public long EditorNameId { get; set; }
        public long RolloverTextId { get; set; }
        public long ShortRolloverTextId { get; set; }
        public double ObstructionRadiusX { get; set; }
        public double ObstructionRadiusZ { get; set; }
        public double MaxVelocity { get; set; }
        public double MaxRunVelocity { get; set; }
        public string MovementType { get; set; }
        public double Lifespan { get; set; }
        public double Los { get; set; }
        public string SoundFile { get; set; }
        public string Decay { get; set; }
        public string Flag { get; set; }
        public string AnimFile { get; set; }
        public double VisualScale { get; set; }
        public string UnitType { get; set; }
        public long ImpactType { get; set; }
        public string Icon { get; set; }
        public string PortraitIcon { get; set; }
        public double InitialHitpoints { get; set; }
        public double MaxHitpoints { get; set; }
        public double Bounty { get; set; }
        public string InitialResource { get; set; }
        public long ResourceSubType { get; set; }
        public string MinimapColor { get; set; }
        public long GathererLimit { get; set; }
        public double TurnRate { get; set; }
        public long WanderDistance { get; set; }
        public string UnitAiType { get; set; }
        public string Event { get; set; }
        public string FormationCategory { get; set; }
        public long PhysicsInfo { get; set; }
        public long SelectionPriority { get; set; }
        public string InitialUnitAiStance { get; set; }
        public string Tactics { get; set; }
        public long PopulationCapAddition { get; set; }
        public string PlacementFile { get; set; }
        public double BuildPoints { get; set; }
        public double BuildingWorkRate { get; set; }
        public long AllowedAge { get; set; }
        public long BuilderLimit { get; set; }
        public string Contain { get; set; }
        public string Command { get; set; }
        public long UnitLevel { get; set; }
        public string Trait1 { get; set; }
        public string Trait2 { get; set; }
        public string Trait3 { get; set; }
        public string Trait4 { get; set; }
        public string Trait5 { get; set; }
        public string VanTrait1 { get; set; }
        public string VanTrait2 { get; set; }
        public string VanTrait3 { get; set; }
        public string Cost { get; set; }
        public string CarryCapacity { get; set; }
        public double RepairPoints { get; set; }
        public string Tech { get; set; }
        public double IdleTimeout { get; set; }
        public string SocketUnitType { get; set; }
        public double AllowedHeightVariance { get; set; }
        public string MinimapIcon { get; set; }
        public string ProjectileProtoUnit { get; set; }
        public long BuildLimit { get; set; }
        public long MaxContained { get; set; }
        public string InputContext { get; set; }
        public string Train { get; set; }
        public long PopulationCount { get; set; }
        public double CorpseDecalTime { get; set; }
        public double TrainPoints { get; set; }
        public double AutoAttackRange { get; set; }
        public double TurnRadius { get; set; }
        public string HeightBob { get; set; }
        public string DesignTag { get; set; }
        public string DeathMessage { get; set; }
    }
}